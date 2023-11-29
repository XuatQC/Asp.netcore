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
-- Table structure for table `tbl_gfa_past`
--

DROP TABLE IF EXISTS `tbl_gfa_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `プラント名` varchar(10) DEFAULT NULL,
  `年` int DEFAULT NULL,
  `種類` varchar(3) DEFAULT NULL,
  `分野` varchar(4) DEFAULT NULL,
  `個別ID` varchar(3) DEFAULT NULL,
  `通番` int DEFAULT NULL,
  `リビジョン` int DEFAULT NULL,
  `達成目標` varchar(10) DEFAULT NULL,
  `タイトル` varchar(1000) DEFAULT NULL,
  `タイトル_訳文` varchar(4000) DEFAULT NULL,
  `部分翻訳` tinyint DEFAULT '0',
  `VAL完了` tinyint DEFAULT '0',
  `全般_JDC` varchar(1000) DEFAULT NULL,
  `最終版とする` tinyint DEFAULT '0',
  `翻訳範囲` varchar(10) DEFAULT '2',
  `翻訳希望期限` varchar(30) DEFAULT NULL,
  `翻訳言語` varchar(1) DEFAULT NULL,
  `翻訳状況依頼` tinyint DEFAULT '0',
  `翻訳状況依頼_個別ID` varchar(3) DEFAULT NULL,
  `翻訳依頼日` datetime DEFAULT NULL,
  `翻訳状況翻訳中` tinyint DEFAULT '0',
  `翻訳状況翻訳中_個別ID` varchar(3) DEFAULT NULL,
  `翻訳状況翻訳済` tinyint DEFAULT '0',
  `翻訳状況翻訳済_個別ID` varchar(3) DEFAULT NULL,
  `承認状況レビュワ` tinyint DEFAULT '0',
  `承認状況レビュワ_個別ID` varchar(3) DEFAULT NULL,
  `承認状況レビュワ_リビジョン` int DEFAULT NULL,
  `承認状況レビュワ_状況` varchar(10) DEFAULT NULL,
  `承認状況審1` tinyint DEFAULT '0',
  `承認状況審1_個別ID` varchar(3) DEFAULT NULL,
  `承認状況審1_リビジョン` int DEFAULT NULL,
  `承認状況審2` tinyint DEFAULT '0',
  `承認状況審2_個別ID` varchar(3) DEFAULT NULL,
  `承認状況審2_リビジョン` int DEFAULT NULL,
  `承認状況審3` tinyint DEFAULT '0',
  `承認状況審3_個別ID` varchar(3) DEFAULT NULL,
  `承認状況審3_リビジョン` int DEFAULT NULL,
  `承認状況TL` tinyint DEFAULT '0',
  `承認状況TL_個別ID` varchar(3) DEFAULT NULL,
  `承認状況TL_リビジョン` int DEFAULT NULL,
  `承認状況TL_状況` varchar(25) DEFAULT NULL,
  `英語版` varchar(1) DEFAULT NULL,
  `開催中` tinyint DEFAULT '-1',
  `最新フラグ` tinyint DEFAULT '-1',
  `削除フラグ` tinyint DEFAULT '0',
  `編集者` varchar(3) DEFAULT NULL,
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
