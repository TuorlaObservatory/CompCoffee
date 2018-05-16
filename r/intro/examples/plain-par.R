library(rjags)    # Loads library for R <--> JAGS interoperability
library(foreach)  # foreach loops
library(parallel) # basic parallel functionality
library(doSNOW)   # back-end I use, plenty are avaialble on linux

JAGSDemoParallel <- function(){
    
    # Number of iterations & chains
    n_iter <- 1e4
    n_ch <- 6

    # -------------------- PARALLEL MOD STARTS --------------------------------------
    # We run 1 chain per proc, at most n_cores - 1 procs 
    # (otherwise system may freeze until simulation is finished)
    n_proc <- parallel::detectCores() - 1
    # If we have a 2 CPU PC or less, it is bad idea to run in parallel
    if (n_proc < 2) {
        # Define our own operator %op% to represent serial %do%
        `%op%` <- `%do%`
        # Flag that there is no parallel execution
        isParallel <- FALSE
    } else {
        # Define our own operator %oP% to represent parallel %dopar%
        `%op%` <- `%dopar%`
        # Flag that work is done in parallel
        isParallel <- TRUE
        # Run separate processes. If n_ch < n_proc, dont create additional instances
        cl <- makeCluster(min(n_proc, n_ch), "SOCK")
        # Register cluster for execution
        registerDoSNOW(cl)  
    }

    # Loads random number generator module
    # It can produce any number of RNG seeds for any number of
    # chains
    load.module("lecuyer")
    # Generate one seed for each chain (return list of lists)
    rngs <- parallel.seeds("lecuyer::RngStream", n_ch)
    # -------------------- PARALLEL MOD ENDS -----------------------------------------

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
    
    # -------------------- PARALLEL MOD STARTS --------------------------------------
    
    # Names of variables to sample
    varNames <- c("a", "b", "scat")

    # Use foreach with custom-defined %op% 
    # Store result into smpl
    # Iterate over two variables: i from 1 to n_ch
    #                             init - RNG seeds per each chain
    # Also load `rjags` package in each R instance
    smpl <- foreach(i = 1:n_ch, init = rngs,
        .packages = "rjags") %op% {
        # Loads module so rjags know how to deal with RNG seeds
        load.module("lecuyer")
        # Connection simulates file access
        con <- textConnection(modelStr)
        # Model is created and burned `n_iter` iterations
        # n.chain is now 1 - one chain per loop cycle
        mdl <- jags.model(file = con, 
            data = input, inits = init, 
            n.chain = 1, n.adapt = n_iter)
        # Disposing resources
        close(con)

        # Updates model, spinning for additional n_iter per each chain
        update(mdl, n_iter)

        # Samples n_iter measurements per each variable
        # It return a list of length 1 (we asked for one chain)
        # [[1]] returns not the whole list, but only first element of the list
        # it is of class "mcmc" (Peek using `class(variable)`)
        coda.samples(mdl, varNames, n_iter)[[1]]
    }
    # smpl is a list of n_ch elements, each of which is an "mcmc" object
    # Sets class of smpl to "mcmc.list" so other methods now know that
    # it is a result of MCMC simualtion with multiple chains
    class(smpl) <- "mcmc.list"
    # -------------------- PARALLEL MOD ENDS -----------------------------------------
    
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

    # -------------------- PARALLEL MOD STARTS --------------------------------------
    # If we used parallel execution, 
    # deallocate cluster
    if(isParallel)
        stopCluster(cl)
    # -------------------- PARALLEL MOD ENDS -----------------------------------------
        
}


JAGSDemoParallel()

