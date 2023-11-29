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
-- Table structure for table `tbl_reviewmember`
--

DROP TABLE IF EXISTS `tbl_reviewmember`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_reviewmember` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `id_plant` int DEFAULT NULL COMMENT 'ID_Plant',
  `plant_name` varchar(50) DEFAULT NULL COMMENT 'プラント名',
  `initial` varchar(3) DEFAULT NULL COMMENT 'イニシャル',
  `name` varchar(50) DEFAULT NULL COMMENT '氏名',
  `roma_name` varchar(50) DEFAULT NULL COMMENT 'ローマ字',
  `respns_area` varchar(4) DEFAULT NULL COMMENT '担当分野',
  `language` varchar(1) DEFAULT NULL COMMENT '言語',
  `coordinator` tinyint DEFAULT '0' COMMENT '調整員',
  `tl` tinyint DEFAULT '0' COMMENT 'TL',
  `judge` tinyint DEFAULT '0' COMMENT '審査者',
  `editor` tinyint DEFAULT '0' COMMENT '編集者',
  `trans` tinyint DEFAULT '0' COMMENT '翻訳者',
  `judge1` varchar(3) DEFAULT NULL COMMENT '承認者_審1',
  `judge2` varchar(3) DEFAULT NULL COMMENT '承認者_審2',
  `judge3` varchar(3) DEFAULT NULL COMMENT '承認者_審3',
  `tl_judge` varchar(3) DEFAULT NULL COMMENT '承認者_TL',
  `comment` varchar(255) DEFAULT NULL COMMENT 'コメント',
  `last_update` datetime DEFAULT NULL COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='ピアレビューメンバマスタ';
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-19 12:56:04
