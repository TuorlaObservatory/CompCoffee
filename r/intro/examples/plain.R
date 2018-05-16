library(rjags)  # Loads library for R <--> JAGS interoperability


JAGSDemo <- function(){
    
    # Number of iterations & chains
    n_iter <- 1e4
    n_ch <- 3
    
    # Genreating 100 data points
    n <- 100L
    # Linear model function
    modelF <- function(z) 0.23 * z - 64 
    # Simulated data with random noise
    data <- data.frame(x = 1:n)
    data$y = modelF(data$x) + rnorm(nrow(data), 0, 1) * 3

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
    combResult <- as.data.frame(do.call("rbind", smpl))
    names(combResult) <- varNames

    # Calculate statistics per each variable
    statList <- lapply(combResult, function(x) 
        return(c("M" = mean(x), "S" = sd(x), quantile(x, prob = c(0.16, 0.84)))))

    stat <- as.data.frame(do.call(rbind, statList))

    # Fitted function, p is parameter obtained in simulations
    fittedF <- function(z, p) p[1] * z + p[2]
    # Reconstructing our data
    data$y_fit <- fittedF(data$x, stat$M)
    # Estimated intrinsic scatter
    scat <- stat["scat", "M"]

    # Opens new `device` (window)
    dev.new()
    # 'Empty' plot to define plot areas
    plot(NA,
        xlim = range(data$x), ylim = range(data$y),
        xlab = "Argument x", ylab = "Observation y")
    
    # Highlighting area of intrinsic scatter around the model
    polygon(c(data$x, rev(data$x)), 
        c(data$y_fit - scat, rev(data$y_fit + scat)), 
        col = "#40FF40A0", lty = 0)
    
    # Source data
    points(data$x, data$y, pch = 19, cex = 1.25)

    # Model function
    lines(data$x, modelF(data$x), lty = 2, col = "red", lwd = 2)

    # Fitted function
    lines(data$x, data$y_fit, lty = 1, col = "blue", lwd = 2)

    # Prints out statistical results
    print(stat)
}


JAGSDemo()