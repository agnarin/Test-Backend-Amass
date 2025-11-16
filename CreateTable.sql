USE testeaxample;
CREATE TABLE IF NOT EXISTS comments (

    `Id` INT NOT NULL AUTO_INCREMENT,

    `User` VARCHAR(100) NOT NULL,

    `Text` TEXT NOT NULL,

    PRIMARY KEY (`Id`)
)
ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_general_ci;