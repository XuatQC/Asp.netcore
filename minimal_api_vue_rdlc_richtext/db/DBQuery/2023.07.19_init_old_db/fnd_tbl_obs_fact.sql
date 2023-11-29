-- MySQL dump 10.13  Distrib 8.0.29, for Win64 (x86_64)
--
-- Host: 192.168.10.32    Database: fnd
-- ------------------------------------------------------
-- Server version	8.0.33

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_obs_fact`
--

DROP TABLE IF EXISTS `tbl_obs_fact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs_fact` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `quantity` int DEFAULT '1' COMMENT '個数',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `fact` mediumtext COMMENT '事実',
  `fact_trans` mediumtext COMMENT '事実_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` varchar(120) DEFAULT '0' COMMENT '部分翻訳',
  `plus_neutral_delta` tinyint(1) DEFAULT '3' COMMENT 'PLUS_NEUTRAL_DELTA',
  `val_comp` tinyint(1) DEFAULT '0' COMMENT 'VAL完了',
  `ofer_field1` tinyint(1) DEFAULT '0' COMMENT '提供先分野1',
  `ofer_field2` tinyint(1) DEFAULT '0' COMMENT '提供先分野2',
  `ofer_field3` tinyint(1) DEFAULT '0' COMMENT '提供先分野3',
  `ofer_field4` tinyint(1) DEFAULT '0' COMMENT '提供先分野4',
  `ofer_field5` tinyint(1) DEFAULT '0' COMMENT '提供先分野5',
  `ofer_field6` tinyint(1) DEFAULT '0' COMMENT '提供先分野6',
  `ofer_field7` tinyint(1) DEFAULT '0' COMMENT '提供先分野7',
  `ofer_field8` tinyint(1) DEFAULT '0' COMMENT '提供先分野8',
  `ofer_field9` tinyint(1) DEFAULT '0' COMMENT '提供先分野9',
  `ofer_field10` tinyint(1) DEFAULT '0' COMMENT '提供先分野10',
  `ofer_field11` tinyint(1) DEFAULT '0' COMMENT '提供先分野11',
  `poc1` tinyint(1) DEFAULT NULL COMMENT 'POC1',
  `poc2` tinyint(1) DEFAULT NULL COMMENT 'POC2',
  `poc3` tinyint(1) DEFAULT NULL COMMENT 'POC3',
  `poc4` tinyint(1) DEFAULT NULL COMMENT 'POC4',
  `poc5` tinyint(1) DEFAULT NULL COMMENT 'POC5',
  `poc6` tinyint(1) DEFAULT NULL COMMENT 'POC6',
  `poc7` tinyint(1) DEFAULT NULL COMMENT 'POC7',
  `poc8` tinyint(1) DEFAULT NULL COMMENT 'POC8',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  `last_update` datetime DEFAULT NULL COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBC観察事実テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-19 12:55:58
