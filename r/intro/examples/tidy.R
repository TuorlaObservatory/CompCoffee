library(rjags)  # Loads library for R <--> JAGS interoperability
library(tidyverse) # Library for data manipulation & plotting

JAGSDemoTidy <- function() {
    
    # Number of iterations & chains
    n_iter <- 1e4
    n_ch <- 3L
    
    # Genreating 100 data points
    n <- 100L
    # Linear model function
    modelF <- function(z) 0.23 * z - 64 
    # Simulated data with random noise
    data <- tibble(x = 1.0 * (1:n)) %>%
        mutate(y = modelF(x)  + rnorm(nrow(.), 0, 1) * 3)
    
    # JAGS model as a string; Can also be loaded from file
    modelStr <- 
        "model{
            a ~ dunif(-1, 1)
            b ~ dunif(-100, 100)
            scat ~ dunif(0, 5)

            for(i in 1:length(obs)){
                mdl[i] = a * x[i] + b
                obs[i] ~ dnorm(mdl[i], pow(scat, -2))
            }
        }
        "
    # JAGS input data
    input <- list(
        x = data$x,
        obs = data$y
    )

    # Connection simulates file access
    con <- textConnection(modelStr)
    # Model is created and burned `n_iter` iterations
    mdl <- jags.model(con, input, n.chain = n_ch, n.adapt = n_iter)
    # Disposing resources
    close(con)

    # Updates model, spinning for additional n_iter per each chain
    update(mdl, n_iter)

    # Names of variables to sample
    varNames <- c("a", "b", "scat")
    
    # Samples n_iter measurements per each variable per each chain
    smpl <- coda.samples(mdl, varNames, n_iter)
    
    # Debug plot of posteriors
    plot(smpl)

    # Combine chains into one data frame
    combResult <- smpl %>%
        do.call("rbind", .) %>%
        as.tibble %>%
        setNames(., nm = varNames) 

    # Calculate statistics per each variable
    stat <- combResult %>% 
        sapply(FUN = function(x)
            return(c("M" = mean(x), "S" = sd(x), 
                quantile(x, prob = c(0.16, 0.84))))) %>%
        t %>%
        as.tibble  %>% 
        mutate(Var = varNames) %>%
        select(Var, everything())

    # Fitted function, p is parameter obtained in simulations
    fittedF <- function(z, p) p[1] * z + p[2]
    # Reconstructing our data
    data <- data %>%
        mutate(y_fit = fittedF(x, stat %>% pull(M))) %>%
        mutate(y_mdl = modelF(x))
    # Estimated intrinsic scatter
    scat <- stat %>% filter(Var == "scat") %>% pull(M)

    # Constructing a ggplot object
    plt <- data %>% ggplot(aes(x, y)) + # Send data to ggplot
        theme_bw() +                    # Apply empty theme
        # Area of intrinsic scatter, bottom most layer
        geom_ribbon(aes(x, ymin = y_fit - scat, ymax = y_fit + scat),
            fill = "#40FF40", alpha = 0.63) + 
        # Source data, points
        geom_point(size = 2) +
        # Model function, line 
        geom_line(aes(x, y_mdl), 
            colour = "red", size = 1.25, linetype = 2) +
        # Fitted function, line
        geom_line(aes(x, y_fit), 
            colour = "blue", size = 1.25) + 
        # Axis labels
        xlab("Argument x") + 
        ylab("Observation y")
       
    # Opens new `device` (window)
    dev.new()
    # Plots created plot on the new device
    plt %>% print

    # Prints out statistical results
    stat %>% print
}

JAGSDemoTidy()