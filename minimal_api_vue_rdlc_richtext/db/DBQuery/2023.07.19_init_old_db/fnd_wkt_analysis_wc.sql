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
-- Table structure for table `wkt_analysis_wc`
--

DROP TABLE IF EXISTS `wkt_analysis_wc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_analysis_wc` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT '' COMMENT 'プラント名',
  `num` varchar(50) DEFAULT '' COMMENT '番号',
  `Description` mediumtext COMMENT '記述',
  `Description_trans` mediumtext COMMENT '記述_訳文',
  `field` varchar(4) DEFAULT '' COMMENT '分野',
  `part_id` varchar(3) DEFAULT '' COMMENT '個別ID',
  `branch_num` int DEFAULT NULL COMMENT '通番',
  `plus_delta` mediumint DEFAULT NULL COMMENT 'PlusDelta',
  `field_etc` varchar(255) DEFAULT '' COMMENT '分類その他',
  `field_etc_trans` varchar(255) DEFAULT '' COMMENT '分類その他_訳文',
  `tl_approval` tinyint DEFAULT '0' COMMENT 'TL承認',
  `ofer_field1` tinyint DEFAULT '0' COMMENT '提供先分野1',
  `ofer_field2` tinyint DEFAULT '0' COMMENT '提供先分野2',
  `ofer_field3` tinyint DEFAULT '0' COMMENT '提供先分野3',
  `ofer_field4` tinyint DEFAULT '0' COMMENT '提供先分野4',
  `ofer_field5` tinyint DEFAULT '0' COMMENT '提供先分野5',
  `ofer_field6` tinyint DEFAULT '0' COMMENT '提供先分野6',
  `ofer_field7` tinyint DEFAULT '0' COMMENT '提供先分野7',
  `ofer_field8` tinyint DEFAULT '0' COMMENT '提供先分野8',
  `ofer_field9` tinyint DEFAULT '0' COMMENT '提供先分野9',
  `ofer_field10` tinyint DEFAULT '0' COMMENT '提供先分野10',
  `ofer_field11` tinyint DEFAULT '0' COMMENT '提供先分野11',
  `category_set_name` varchar(30) DEFAULT '' COMMENT 'カテゴリセット名',
  `category1` tinyint DEFAULT '0' COMMENT 'カテゴリ1',
  `category2` tinyint DEFAULT '0' COMMENT 'カテゴリ2',
  `category3` tinyint DEFAULT '0' COMMENT 'カテゴリ3',
  `category4` tinyint DEFAULT '0' COMMENT 'カテゴリ4',
  `category5` tinyint DEFAULT '0' COMMENT 'カテゴリ5',
  `category6` tinyint DEFAULT '0' COMMENT 'カテゴリ6',
  `category7` tinyint DEFAULT '0' COMMENT 'カテゴリ7',
  `category8` tinyint DEFAULT '0' COMMENT 'カテゴリ8',
  `category9` tinyint DEFAULT '0' COMMENT 'カテゴリ9',
  `category10` tinyint DEFAULT '0' COMMENT 'カテゴリ10',
  `default_category` varchar(30) DEFAULT '' COMMENT 'デフォルトカテゴリ',
  `first_category` varchar(30) DEFAULT '' COMMENT '第1カテゴリ',
  `second_category1` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_1',
  `second_category2` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_2',
  `second_category3` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_3',
  `second_category4` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_4',
  `second_category5` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_5',
  `second_category6` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_6',
  `second_category7` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_7',
  `second_category8` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_8',
  `second_category9` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_9',
  `second_category10` varchar(30) DEFAULT '' COMMENT '第2カテゴリ_10',
  `third_category1` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_1',
  `third_category2` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_2',
  `third_category3` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_3',
  `third_category4` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_4',
  `third_category5` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_5',
  `third_category6` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_6',
  `third_category7` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_7',
  `third_category8` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_8',
  `third_category9` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_9',
  `third_category10` varchar(30) DEFAULT '' COMMENT '第3カテゴリ_10',
  `copy_flg` tinyint DEFAULT NULL COMMENT 'コピーフラグ',
  `english_edition` varchar(1) DEFAULT '' COMMENT '英語版',
  `field_order` int DEFAULT NULL COMMENT '分野並び',
  `editor` varchar(3) DEFAULT '' COMMENT '編集者',
  `list_update` datetime DEFAULT NULL COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_Analysis_WC';
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-19 12:55:54
