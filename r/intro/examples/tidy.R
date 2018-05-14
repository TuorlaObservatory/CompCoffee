library(rjags)
library(tidyverse)

JAGSDemoTidy <- function() {
    
    # Number of iterations and chains
    n_iter <- 1e4
    n_ch <- 3L
    
    #Generating data
    n <- 100L
    modelF <- function(z) 0.23 * z - 64 
    data <- tibble(x = 1.0 * (1:n)) %>%
        mutate(y = modelF(x + abs(x - mean(x)) * ( rnorm(nrow(.), 0, 1)))) 
    
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

    input <- list(
        x = data$x,
        obs = data$y
    )

    con <- textConnection(modelStr)
    mdl <- jags.model(con, input, n.chain = n_ch, n.adapt = n_iter)
    close(con)

    update(mdl, n_iter)

    varNames <- c("a", "b", "scat")
    
    smpl <- coda.samples(mdl, varNames, n_iter)
    
    combResult <- smpl %>%
        do.call("rbind", .) %>%
        as.tibble %>%
        setNames(., nm = varNames) 

    stat <- combResult %>% 
        sapply(FUN = function(x)
            return(c("M" = mean(x), "S" = sd(x), 
                quantile(x, prob = c(0.16, 0.84))))) %>%
        t %>%
        as.tibble  %>% 
        mutate(Var = varNames) %>%
        select(Var, everything())

    fittedF <- function(z, p) p[1] * z + p[2]
    data <- data %>%
        mutate(y_fit = fittedF(x, stat %>% pull(M))) %>%
        mutate(y_mdl = modelF(x))

    scat <- stat %>% filter(Var == "scat") %>% pull(M)

    plt <- data %>% ggplot(aes(x, y)) +
        theme_bw() +
        geom_ribbon(aes(x, ymin = y_fit - scat, ymax = y_fit + scat),
            fill = "#40FF40", alpha = 0.63) +
        geom_point(size = 2) + 
        geom_line(aes(x, y_mdl), 
            colour = "red", size = 1.25, linetype = 2) +
        geom_line(aes(x, y_fit), 
            colour = "blue", size = 1.25) + 
        xlab("Argument x") + 
        ylab("Observation y")
       

    plt %>% print
    stat %>% print
}

JAGSDemoTidy()