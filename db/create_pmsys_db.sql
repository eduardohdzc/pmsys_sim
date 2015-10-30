-- MySQL Script generated by MySQL Workbench
-- 10/27/15 21:02:14
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema pmsys_db
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `pmsys_db` ;

-- -----------------------------------------------------
-- Schema pmsys_db
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `pmsys_db` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `pmsys_db` ;

-- -----------------------------------------------------
-- Table `pmsys_db`.`users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `pmsys_db`.`users` ;

CREATE TABLE IF NOT EXISTS `pmsys_db`.`users` (
  `usr_id` INT NOT NULL AUTO_INCREMENT COMMENT '',
  `usr_name` VARCHAR(45) NOT NULL COMMENT '',
  `usr_pswd` VARCHAR(45) NOT NULL COMMENT '',
  `usr_status` TINYINT(1) NOT NULL COMMENT '',
  `usr_privileges` VARCHAR(45) NOT NULL COMMENT '',
  PRIMARY KEY (`usr_id`)  COMMENT '')
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `pmsys_db`.`projects`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `pmsys_db`.`projects` ;

CREATE TABLE IF NOT EXISTS `pmsys_db`.`projects` (
  `prj_id` INT NOT NULL AUTO_INCREMENT COMMENT '',
  `prj_name` VARCHAR(45) NOT NULL COMMENT '',
  `prj_description` VARCHAR(45) NULL COMMENT '',
  `prj_status` TINYINT(1) NOT NULL COMMENT '',
  PRIMARY KEY (`prj_id`)  COMMENT '')
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `pmsys_db`.`projects_has_users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `pmsys_db`.`projects_has_users` ;

CREATE TABLE IF NOT EXISTS `pmsys_db`.`projects_has_users` (
  `projects_prj_id` INT NOT NULL COMMENT '',
  `users_usr_id` INT NOT NULL COMMENT '',
  `prj_usr_role` VARCHAR(45) NOT NULL COMMENT '',
  PRIMARY KEY (`projects_prj_id`, `users_usr_id`)  COMMENT '',
  INDEX `fk_projects_has_users_users1_idx` (`users_usr_id` ASC)  COMMENT '',
  INDEX `fk_projects_has_users_proyects_idx` (`projects_prj_id` ASC)  COMMENT '',
  CONSTRAINT `fk_proyects_has_users_proyects`
    FOREIGN KEY (`projects_prj_id`)
    REFERENCES `pmsys_db`.`projects` (`prj_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_proyects_has_users_users1`
    FOREIGN KEY (`users_usr_id`)
    REFERENCES `pmsys_db`.`users` (`usr_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `pmsys_db`.`activities`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `pmsys_db`.`activities` ;

CREATE TABLE IF NOT EXISTS `pmsys_db`.`activities` (
  `act_id` INT NOT NULL AUTO_INCREMENT COMMENT '',
  `act_name` VARCHAR(45) NOT NULL COMMENT '',
  `act_description` TINYTEXT NULL COMMENT '',
  `act_planned_start` DATE NOT NULL COMMENT '',
  `act_planned_finish` DATE NOT NULL COMMENT '',
  `act_real_start` DATE NULL COMMENT '',
  `act_real_finish` DATE NULL COMMENT '',
  `projects_prj_id` INT NOT NULL COMMENT '',
  PRIMARY KEY (`act_id`, `projects_prj_id`)  COMMENT '',
  INDEX `fk_activities_projects1_idx` (`projects_prj_id` ASC)  COMMENT '',
  CONSTRAINT `fk_activities_proyects1`
    FOREIGN KEY (`projects_prj_id`)
    REFERENCES `pmsys_db`.`projects` (`prj_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `pmsys_db`.`users_has_activities`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `pmsys_db`.`users_has_activities` ;

CREATE TABLE IF NOT EXISTS `pmsys_db`.`users_has_activities` (
  `users_usr_id` INT NOT NULL COMMENT '',
  `activities_act_id` INT NOT NULL COMMENT '',
  `activities_projects_prj_id` INT NOT NULL COMMENT '',
  `usr_act_progress` VARCHAR(45) NULL COMMENT '',
  `usr_act_comments` VARCHAR(45) NULL COMMENT '',
  PRIMARY KEY (`users_usr_id`, `activities_act_id`, `activities_projects_prj_id`)  COMMENT '',
  INDEX `fk_users_has_activities_activities1_idx` (`activities_act_id` ASC, `activities_projects_prj_id` ASC)  COMMENT '',
  INDEX `fk_users_has_activities_users1_idx` (`users_usr_id` ASC)  COMMENT '',
  CONSTRAINT `fk_users_has_activities_users1`
    FOREIGN KEY (`users_usr_id`)
    REFERENCES `pmsys_db`.`users` (`usr_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_users_has_activities_activities1`
    FOREIGN KEY (`activities_act_id` , `activities_projects_prj_id`)
    REFERENCES `pmsys_db`.`activities` (`act_id` , `projects_prj_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `pmsys_db` ;

-- -----------------------------------------------------
-- Placeholder table for view `pmsys_db`.`view1`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `pmsys_db`.`view1` (`prj_id` INT, `prj_name` INT, `prj_description` INT, `prj_status` INT);

-- -----------------------------------------------------
-- View `pmsys_db`.`view1`
-- -----------------------------------------------------
DROP VIEW IF EXISTS `pmsys_db`.`view1` ;
DROP TABLE IF EXISTS `pmsys_db`.`view1`;
USE `pmsys_db`;
CREATE  OR REPLACE VIEW `view1` AS
    SELECT 
        *
    FROM
        projects;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
