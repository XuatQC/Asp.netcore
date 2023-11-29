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
-- Table structure for table `tbl_analysis_obs`
--

DROP TABLE IF EXISTS `tbl_analysis_obs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_analysis_obs` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT '' COMMENT 'プラント名',
  `kinds` varchar(3) DEFAULT '' COMMENT '種類',
  `fields` varchar(4) DEFAULT '' COMMENT '分野',
  `part_id` varchar(3) DEFAULT '' COMMENT '個別ID',
  `branch_num` int DEFAULT '0' COMMENT '通番',
  `revisions` int DEFAULT '0' COMMENT 'リビジョン',
  `english_edition` varchar(1) DEFAULT '' COMMENT '英語版',
  `num` varchar(50) DEFAULT '' COMMENT '番号',
  `last_package` varchar(10) DEFAULT '' COMMENT '最終パッケージ',
  `editor` varchar(3) DEFAULT '' COMMENT '編集者',
  `quantity` int DEFAULT '1' COMMENT '個数',
  `serial_num` varchar(255) DEFAULT '' COMMENT 'No',
  `seq_num` varchar(255) DEFAULT '' COMMENT 'SEQNo',
  `fact` mediumtext COMMENT '事実',
  `fact_trans` mediumtext COMMENT '事実_訳文',
  `plus_neutral_delta` varchar(255) DEFAULT '' COMMENT 'PLUS_NEUTRAL_DELTA',
  `val_comp` varchar(255) DEFAULT '' COMMENT 'VAL完了',
  `tl_approval` varchar(120) DEFAULT '' COMMENT 'TL承認',
  `ofer_field1` varchar(255) DEFAULT '' COMMENT '提供先分野1',
  `ofer_field2` varchar(255) DEFAULT '' COMMENT '提供先分野2',
  `ofer_field3` varchar(255) DEFAULT '' COMMENT '提供先分野3',
  `ofer_field4` varchar(255) DEFAULT '' COMMENT '提供先分野4',
  `ofer_field5` varchar(255) DEFAULT '' COMMENT '提供先分野5',
  `ofer_field6` varchar(255) DEFAULT '' COMMENT '提供先分野6',
  `ofer_field7` varchar(255) DEFAULT '' COMMENT '提供先分野7',
  `ofer_field8` varchar(255) DEFAULT '' COMMENT '提供先分野8',
  `ofer_field9` varchar(255) DEFAULT '' COMMENT '提供先分野9',
  `ofer_field10` varchar(255) DEFAULT '' COMMENT '提供先分野10',
  `ofer_field11` varchar(255) DEFAULT '' COMMENT '提供先分野11',
  `poc1` varchar(255) DEFAULT '' COMMENT 'POC1',
  `poc2` varchar(255) DEFAULT '' COMMENT 'POC2',
  `poc3` varchar(255) DEFAULT '' COMMENT 'POC3',
  `poc4` varchar(255) DEFAULT '' COMMENT 'POC4',
  `poc5` varchar(255) DEFAULT '' COMMENT 'POC5',
  `poc6` varchar(255) DEFAULT '' COMMENT 'POC6',
  `poc7` varchar(255) DEFAULT '' COMMENT 'POC7',
  `poc8` varchar(255) DEFAULT '' COMMENT 'POC8',
  `category_set_name` varchar(30) DEFAULT '' COMMENT 'カテゴリセット名',
  `first_category` mediumtext COMMENT '第1カテゴリ',
  `second_category1` mediumtext COMMENT '第2カテゴリ_1',
  `second_category2` mediumtext COMMENT '第2カテゴリ_2',
  `second_category3` mediumtext COMMENT '第2カテゴリ_3',
  `second_category4` mediumtext COMMENT '第2カテゴリ_4',
  `second_category5` mediumtext COMMENT '第2カテゴリ_5',
  `second_category6` mediumtext COMMENT '第2カテゴリ_6',
  `second_category7` mediumtext COMMENT '第2カテゴリ_7',
  `second_category8` mediumtext COMMENT '第2カテゴリ_8',
  `second_category9` mediumtext COMMENT '第2カテゴリ_9',
  `second_category10` mediumtext COMMENT '第2カテゴリ_10',
  `third_category1` mediumtext COMMENT '第3カテゴリ_1',
  `third_category2` mediumtext COMMENT '第3カテゴリ_2',
  `third_category3` mediumtext COMMENT '第3カテゴリ_3',
  `third_category4` mediumtext COMMENT '第3カテゴリ_4',
  `third_category5` mediumtext COMMENT '第3カテゴリ_5',
  `third_category6` mediumtext COMMENT '第3カテゴリ_6',
  `third_category7` mediumtext COMMENT '第3カテゴリ_7',
  `third_category8` mediumtext COMMENT '第3カテゴリ_8',
  `third_category9` mediumtext COMMENT '第3カテゴリ_9',
  `third_category10` mediumtext COMMENT '第3カテゴリ_10',
  `last_update` datetime DEFAULT NULL COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBS分析テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-19 12:55:57
