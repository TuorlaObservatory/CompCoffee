library(rjags)  # Loads library for R <--> JAGS interoperability

JAGSDemo <- function(){
    
    # Number of iterations
    n_iter <- 1e4
    n_ch <- 3
    # Genreating data
    n <- 100L
    modelF <- function(z) 0.23 * z - 64 
    data <- data.frame(x = 1:n)
    data$y = modelF(data$x + abs(data$x - mean(data$x))*rnorm(nrow(data), 0, 1))

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

    combResult <- as.data.frame(do.call("rbind", smpl))
    names(combResult) <- varNames

    statList <- lapply(combResult, function(x) 
        return(c("M" = mean(x), "S" = sd(x), quantile(x, prob = c(0.16, 0.84)))))

    stat <- as.data.frame(do.call(rbind, statList))

    fittedF <- function(z) stat["a", "M"] * z + stat["b", "M"]
    data$y_fit <- fittedF(data$x)
    scat <- stat["scat", "M"]

    plot(NA,
        xlim = range(data$x), ylim = range(data$y),
        xlab = "Argument x", ylab = "Observation y")
    
    polygon(c(data$x, rev(data$x)), 
        c(data$y_fit - scat, rev(data$y_fit + scat)), 
        col = "#40FF40A0", lty = 0)
    
    points(data$x, data$y, pch = 19, cex = 1.25)

    lines(data$x, modelF(data$x), lty = 2, col = "red", lwd = 2)
    

    lines(data$x, data$y_fit, lty = 1, col = "blue", lwd = 2)



    print(stat)
}


JAGSDemo()