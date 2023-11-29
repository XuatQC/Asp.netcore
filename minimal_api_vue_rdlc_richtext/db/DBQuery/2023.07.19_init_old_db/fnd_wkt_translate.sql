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
-- Table structure for table `wkt_translate`
--

DROP TABLE IF EXISTS `wkt_translate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_translate` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT NULL COMMENT 'プラント名',
  `kinds` varchar(20) DEFAULT '1' COMMENT '種類',
  `fields` varchar(4) DEFAULT NULL COMMENT '分野',
  `part_id` varchar(3) DEFAULT NULL COMMENT '個別ID',
  `branch_num` int DEFAULT NULL COMMENT '通番',
  `revisions` int DEFAULT '0' COMMENT 'リビジョン',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `english_edition` varchar(1) DEFAULT NULL COMMENT '英語版',
  `offer_num` int unsigned DEFAULT NULL COMMENT '依頼番号',
  `title` mediumtext COMMENT 'タイトル',
  `title_trans` mediumtext COMMENT 'タイトル_訳文',
  `ver_trans_done` int DEFAULT NULL COMMENT '最新バージョン_翻訳済',
  `ver_original` int DEFAULT NULL COMMENT '最新バージョン_原文',
  `editor` varchar(3) DEFAULT NULL COMMENT '編集者',
  `trans_situation` varchar(10) DEFAULT NULL COMMENT '翻訳状況',
  `trans_arrival` varchar(1) DEFAULT NULL COMMENT '翻訳到着',
  `trans_cansel` varchar(3) DEFAULT NULL COMMENT '翻訳キャンセル',
  `trans_urgent` varchar(3) DEFAULT NULL COMMENT '翻訳至急',
  `contact_memo` varchar(255) DEFAULT NULL COMMENT '連絡メモ',
  `output_date` datetime DEFAULT NULL COMMENT '出力日',
  `capture_date` datetime DEFAULT NULL COMMENT '取込日',
  `trans_date` datetime DEFAULT NULL COMMENT '翻訳日',
  `translator` varchar(20) DEFAULT NULL COMMENT '翻訳者',
  `cansel_status` varchar(10) DEFAULT NULL COMMENT '取消状況',
  `selection_cansel_status` varchar(10) DEFAULT NULL COMMENT '取消状況選択用',
  `trans_scope` varchar(10) DEFAULT NULL COMMENT '翻訳範囲',
  `trans_deadline` varchar(30) DEFAULT NULL COMMENT '翻訳希望期限',
  `trans_lang` varchar(1) DEFAULT NULL COMMENT '翻訳言語',
  `trans_req_date` datetime DEFAULT NULL COMMENT '翻訳依頼日',
  `file_name` varchar(255) DEFAULT NULL COMMENT 'ファイル名',
  `select_check` tinyint DEFAULT '0' COMMENT '選択チェック',
  `last_update` datetime DEFAULT NULL COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_translate';
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-19 12:56:08
