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
-- Table structure for table `tbl_str`
--

DROP TABLE IF EXISTS `tbl_str`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_str` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT NULL COMMENT 'プラント名',
  `years` int DEFAULT NULL COMMENT '年',
  `kinds` varchar(3) DEFAULT NULL COMMENT '種類',
  `fields` varchar(4) DEFAULT NULL COMMENT '分野',
  `part_id` varchar(3) DEFAULT NULL COMMENT '個別ID',
  `branch_num` int DEFAULT NULL COMMENT '通番',
  `revisions` int DEFAULT NULL COMMENT 'リビジョン',
  `achievement_goal` varchar(10) DEFAULT NULL COMMENT '達成目標',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
  `package_exclude` tinyint DEFAULT '0' COMMENT '最終パッケージに含めない',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `title_trans` varchar(1000) DEFAULT NULL COMMENT 'タイトル_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `general_jdc` varchar(1000) DEFAULT NULL COMMENT '全般_JDC',
  `connect_num` varchar(10) DEFAULT NULL COMMENT '関連番号',
  `connect_memo` varchar(30) DEFAULT NULL COMMENT '関連メモ',
  `connect_memo_trans` varchar(60) DEFAULT NULL COMMENT '関連メモ_訳文',
  `connect_memo_parttrans` tinyint DEFAULT '0' COMMENT '関連メモ_部分翻訳',
  `trans_range` varchar(10) DEFAULT '2' COMMENT '翻訳範囲',
  `trans_deadline` varchar(30) DEFAULT NULL COMMENT '翻訳希望期限',
  `trans_lang` varchar(1) DEFAULT NULL COMMENT '翻訳言語',
  `trans_state_req` tinyint DEFAULT '0' COMMENT '翻訳状況依頼',
  `trans_state_req_id` varchar(3) DEFAULT NULL COMMENT '翻訳状況依頼_個別ID',
  `trans_req_date` datetime DEFAULT NULL COMMENT '翻訳依頼日',
  `translating_state` tinyint DEFAULT '0' COMMENT '翻訳状況翻訳中',
  `translating_state_id` varchar(3) DEFAULT NULL COMMENT '翻訳状況翻訳中_個別ID',
  `translated_state` tinyint DEFAULT '0' COMMENT '翻訳状況翻訳済',
  `translated_state_id` varchar(3) DEFAULT NULL COMMENT '翻訳状況翻訳済_個別ID',
  `approval_state_pr` tinyint DEFAULT '0' COMMENT '承認状況レビュワ',
  `approval_state_pr_id` varchar(3) DEFAULT NULL COMMENT '承認状況レビュワ_個別ID',
  `approval_state_pr_rev` int DEFAULT NULL COMMENT '承認状況レビュワ_リビジョン',
  `approval_state_pr_state` varchar(10) DEFAULT NULL COMMENT '承認状況レビュワ_状況',
  `approval_state1` tinyint DEFAULT '0' COMMENT '承認状況審1',
  `approval_state1_id` varchar(3) DEFAULT NULL COMMENT '承認状況審1_個別ID',
  `approval_state1_rev` int DEFAULT NULL COMMENT '承認状況審1_リビジョン',
  `approval_state2` tinyint DEFAULT '0' COMMENT '承認状況審2',
  `approval_state2_id` varchar(3) DEFAULT NULL COMMENT '承認状況審2_個別ID',
  `approval_state2_rev` int DEFAULT NULL COMMENT '承認状況審2_リビジョン',
  `approval_state3` tinyint DEFAULT '0' COMMENT '承認状況審3',
  `approval_state3_id` varchar(3) DEFAULT NULL COMMENT '承認状況審3_個別ID',
  `approval_state3_rev` int DEFAULT NULL COMMENT '承認状況審3_リビジョン',
  `approval_state_tl` tinyint DEFAULT '0' COMMENT '承認状況TL',
  `approval_state_tl_id` varchar(3) DEFAULT NULL COMMENT '承認状況TL_個別ID',
  `approval_state_tl_rev` int DEFAULT NULL COMMENT '承認状況TL_リビジョン',
  `approval_state_tl_state` varchar(25) DEFAULT NULL COMMENT '承認状況TL_状況',
  `english_edition` varchar(1) DEFAULT NULL COMMENT '英語版',
  `hold` tinyint DEFAULT '-1' COMMENT '開催中',
  `newest_flag` tinyint DEFAULT '-1' COMMENT '最新フラグ',
  `delete_flag` tinyint DEFAULT '0' COMMENT '削除フラグ',
  `editor` varchar(3) DEFAULT NULL COMMENT '編集者',
  `last_update` datetime DEFAULT NULL COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='STRテーブル';
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
