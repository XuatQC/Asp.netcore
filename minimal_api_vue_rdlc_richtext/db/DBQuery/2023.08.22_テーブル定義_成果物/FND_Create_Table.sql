-- MySQL dump 10.13  Distrib 8.0.29, for Win64 (x86_64)
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
-- Table structure for table `tbl_activity`
--

DROP TABLE IF EXISTS `tbl_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_activity` (
  `plant_name` varchar(10) NOT NULL COMMENT 'プラント名',
  `target` varchar(10) NOT NULL COMMENT '対象',
  `target_name` varchar(50) DEFAULT NULL COMMENT '対象名',
  `target_name_en` varchar(50) DEFAULT NULL COMMENT '対象名（英語）',
  `display_order` int DEFAULT NULL COMMENT '表示順',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`plant_name`,`target`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='観察対象マスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_afi`
--

DROP TABLE IF EXISTS `tbl_afi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_afi` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT NULL COMMENT 'プラント名',
  `years` int DEFAULT NULL COMMENT '年',
  `kinds` varchar(3) DEFAULT NULL COMMENT '種類',
  `fields` varchar(4) DEFAULT NULL COMMENT '分野',
  `part_id` varchar(3) DEFAULT NULL COMMENT '個別ID',
  `serial_num` int DEFAULT NULL COMMENT '通番',
  `revisions` int DEFAULT NULL COMMENT 'リビジョン',
  `achievement_goal` varchar(10) DEFAULT NULL COMMENT '達成目標',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `title_trans` varchar(1000) DEFAULT NULL COMMENT 'タイトル_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `observation_target` varchar(2) DEFAULT NULL COMMENT '観察対象',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
  `package_exclude` tinyint DEFAULT '0' COMMENT '最終パッケージに含めない',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了',
  `general_jdc` varchar(1000) DEFAULT NULL COMMENT '全般_JDC',
  `connect_num` varchar(10) DEFAULT NULL COMMENT '関連番号',
  `connect_memo` varchar(100) DEFAULT NULL COMMENT '関連メモ',
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
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='AFIテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_afi_attach`
--

DROP TABLE IF EXISTS `tbl_afi_attach`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_afi_attach` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(5000) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(500) DEFAULT NULL COMMENT '連番',
  `seq_num` varchar(500) DEFAULT NULL COMMENT 'SEQNo',
  `represent_phot` varchar(500) DEFAULT NULL COMMENT '代表写真',
  `file_name` text COMMENT 'ファイル名',
  `size` varchar(9100) DEFAULT NULL COMMENT 'サイズ',
  `state_flag` varchar(500) DEFAULT NULL COMMENT '状態フラグ',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='AFI添付ファイル（写真）テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_afi_currentperspective`
--

DROP TABLE IF EXISTS `tbl_afi_currentperspective`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_afi_currentperspective` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `current` mediumtext COMMENT '現在',
  `current_trans` mediumtext COMMENT '現在_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='AFI取組状況テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_afi_example`
--

DROP TABLE IF EXISTS `tbl_afi_example`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_afi_example` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `quantity` int DEFAULT '1' COMMENT '個数',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `example` mediumtext COMMENT '事例',
  `example_trans` mediumtext COMMENT '事例_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` varchar(120) DEFAULT '0' COMMENT '部分翻訳',
  `val_comp` varchar(120) DEFAULT '0' COMMENT 'VAL完了',
  `val_comp_lock` varchar(120) DEFAULT '0' COMMENT 'VAL完了ロック',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='AFI事例テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_afi_keycauses_past`
--

DROP TABLE IF EXISTS `tbl_afi_keycauses_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_afi_keycauses_past` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `major` mediumtext COMMENT '主要',
  `major_trans` mediumtext COMMENT '主要_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='AFI考察テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

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
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBS分析テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_analysis_wc`
--

DROP TABLE IF EXISTS `tbl_analysis_wc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_analysis_wc` (
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
  `english_edition` varchar(1) DEFAULT '' COMMENT '英語版',
  `editor` varchar(3) DEFAULT '' COMMENT '編集者',
  `list_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='WC分析テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_bp`
--

DROP TABLE IF EXISTS `tbl_bp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_bp` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT '' COMMENT 'プラント名',
  `years` int DEFAULT NULL COMMENT '年',
  `kinds` varchar(3) DEFAULT '' COMMENT '種類',
  `fields` varchar(4) DEFAULT '' COMMENT '分野',
  `part_id` varchar(3) DEFAULT '' COMMENT '個別ID',
  `branch_num` int DEFAULT NULL COMMENT '通番',
  `revisions` int DEFAULT NULL COMMENT 'リビジョン',
  `achievement_goal` varchar(10) DEFAULT '' COMMENT '達成目標',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
  `package_exclude` tinyint DEFAULT '0' COMMENT '最終パッケージに含めない',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了',
  `title` mediumtext COMMENT 'タイトル',
  `title_trans` mediumtext COMMENT 'タイトル_訳文',
  `title_part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳_タイトル',
  `explan_text` mediumtext COMMENT '説明文',
  `explan_text_trans` mediumtext COMMENT '説明文_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `part_trans_explan` tinyint DEFAULT '0' COMMENT '部分翻訳_説明文',
  `explan_text_translation` tinyint DEFAULT '0' COMMENT '説明文_翻訳',
  `explan_text_jdc` varchar(1000) DEFAULT '' COMMENT '説明文_JDC',
  `general_jdc` varchar(1000) DEFAULT '' COMMENT '全般_JDC',
  `connect_num` varchar(10) DEFAULT '' COMMENT '関連番号',
  `connect_memo` varchar(30) DEFAULT '' COMMENT '関連メモ',
  `connect_memo_trans` varchar(60) DEFAULT '' COMMENT '関連メモ_訳文',
  `connect_memo_parttrans` tinyint DEFAULT '0' COMMENT '関連メモ_部分翻訳',
  `trans_range` varchar(10) DEFAULT '2' COMMENT '翻訳範囲',
  `trans_deadline` varchar(30) DEFAULT '' COMMENT '翻訳希望期限',
  `trans_lang` varchar(1) DEFAULT '' COMMENT '翻訳言語',
  `trans_state_req` tinyint DEFAULT '0' COMMENT '翻訳状況依頼',
  `trans_state_req_id` varchar(3) DEFAULT '' COMMENT '翻訳状況依頼_個別ID',
  `trans_req_date` datetime DEFAULT NULL COMMENT '翻訳依頼日',
  `translating_state` tinyint DEFAULT '0' COMMENT '翻訳状況翻訳中',
  `translating_state_id` varchar(3) DEFAULT '' COMMENT '翻訳状況翻訳中_個別ID',
  `translated_state` tinyint DEFAULT '0' COMMENT '翻訳状況翻訳済',
  `translated_state_id` varchar(3) DEFAULT '' COMMENT '翻訳状況翻訳済_個別ID',
  `approval_state_pr` tinyint DEFAULT '0' COMMENT '承認状況レビュワ',
  `approval_state_pr_id` varchar(3) DEFAULT '' COMMENT '承認状況レビュワ_個別ID',
  `approval_state_pr_rev` int DEFAULT NULL COMMENT '承認状況レビュワ_リビジョン',
  `approval_state_pr_state` varchar(10) DEFAULT '' COMMENT '承認状況レビュワ_状況',
  `approval_state1` tinyint DEFAULT '0' COMMENT '承認状況審1',
  `approval_state1_id` varchar(3) DEFAULT '' COMMENT '承認状況審1_個別ID',
  `approval_state1_rev` int DEFAULT NULL COMMENT '承認状況審1_リビジョン',
  `approval_state2` tinyint DEFAULT '0' COMMENT '承認状況審2',
  `approval_state2_id` varchar(3) DEFAULT '' COMMENT '承認状況審2_個別ID',
  `approval_state2_rev` int DEFAULT NULL COMMENT '承認状況審2_リビジョン',
  `approval_state3` tinyint DEFAULT '0' COMMENT '承認状況審3',
  `approval_state3_id` varchar(3) DEFAULT '' COMMENT '承認状況審3_個別ID',
  `approval_state3_rev` int DEFAULT NULL COMMENT '承認状況審3_リビジョン',
  `approval_state_tl` tinyint DEFAULT '0' COMMENT '承認状況TL',
  `approval_state_tl_id` varchar(3) DEFAULT '' COMMENT '承認状況TL_個別ID',
  `approval_state_tl_rev` int DEFAULT NULL COMMENT '承認状況TL_リビジョン',
  `approval_state_tl_state` varchar(25) DEFAULT '' COMMENT '承認状況TL_状況',
  `english_edition` varchar(1) DEFAULT '' COMMENT '英語版',
  `hold` tinyint DEFAULT '-1' COMMENT '開催中',
  `newest_flag` tinyint DEFAULT '-1' COMMENT '最新フラグ',
  `delete_flag` tinyint DEFAULT '0' COMMENT '削除フラグ',
  `editor` varchar(3) DEFAULT '' COMMENT '編集者',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT '' COMMENT '最終更新者',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='BPテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_bp_attach`
--

DROP TABLE IF EXISTS `tbl_bp_attach`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_bp_attach` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(5000) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(500) DEFAULT NULL COMMENT '連番',
  `seq_num` varchar(500) DEFAULT NULL COMMENT 'SEQNo',
  `represent_phot` varchar(500) DEFAULT NULL COMMENT '代表写真',
  `file_name` text COMMENT 'ファイル名',
  `size` varchar(1000) DEFAULT NULL COMMENT 'サイズ',
  `state_flag` varchar(500) DEFAULT NULL COMMENT '状態フラグ',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='BP添付ファイル（写真）テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_category_obs`
--

DROP TABLE IF EXISTS `tbl_category_obs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_category_obs` (
  `plant_name` varchar(10) NOT NULL COMMENT 'プラント名',
  `analysis_file_name` varchar(30) DEFAULT NULL COMMENT '分析ツリーファイル名',
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
  `editor` varchar(3) DEFAULT NULL COMMENT '編集者',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`plant_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBSカテゴリテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_controls`
--

DROP TABLE IF EXISTS `tbl_controls`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_controls` (
  `last_sync_date` datetime NOT NULL COMMENT '最終同期日',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`last_sync_date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_field`
--

DROP TABLE IF EXISTS `tbl_field`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_field` (
  `plant_name` varchar(10) NOT NULL COMMENT 'プラント名',
  `fields` varchar(4) NOT NULL COMMENT '分野',
  `fields_name` varchar(255) DEFAULT NULL COMMENT '分野名',
  `fields_name_en` varchar(255) DEFAULT NULL COMMENT '分野名（英語）',
  `dm_div` varchar(1) DEFAULT NULL COMMENT 'DM区分',
  `display_order` int DEFAULT NULL COMMENT '表示順',
  `output_order` int DEFAULT NULL COMMENT '出力順',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`plant_name`,`fields`),
  KEY `ID_FIELD_PLANT_NAME` (`plant_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='分野マスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa`
--

DROP TABLE IF EXISTS `tbl_gfa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT NULL COMMENT 'プラント名',
  `years` int DEFAULT NULL COMMENT '年',
  `kinds` varchar(3) DEFAULT NULL COMMENT '種類',
  `fields` varchar(4) DEFAULT NULL COMMENT '分野',
  `part_id` varchar(3) DEFAULT NULL COMMENT '個別ID',
  `branch_num` int DEFAULT NULL COMMENT '通番',
  `revisions` int DEFAULT NULL COMMENT 'リビジョン',
  `achievement_goal` varchar(10) DEFAULT NULL COMMENT '達成目標',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `title_trans` varchar(1000) DEFAULT NULL COMMENT 'タイトル_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了',
  `general_jdc` varchar(1000) DEFAULT NULL COMMENT '全般_JDC',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
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
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='GFAテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_attach`
--

DROP TABLE IF EXISTS `tbl_gfa_attach`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_attach` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(5000) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(500) DEFAULT NULL COMMENT '連番',
  `seq_num` varchar(500) DEFAULT NULL COMMENT 'SEQNo',
  `represent_phot` varchar(500) DEFAULT NULL COMMENT '代表写真',
  `file_name` text COMMENT 'ファイル名',
  `size` varchar(1000) DEFAULT NULL COMMENT 'サイズ',
  `state_flag` varchar(500) DEFAULT NULL COMMENT '状態フラグ',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='GFA添付ファイル（写真）テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_attach_past`
--

DROP TABLE IF EXISTS `tbl_gfa_attach_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_attach_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(5000) DEFAULT NULL,
  `連番` varchar(500) DEFAULT NULL,
  `SEQNo` varchar(500) DEFAULT NULL,
  `代表写真` varchar(500) DEFAULT NULL,
  `ファイル名` text,
  `サイズ` varchar(1000) DEFAULT NULL,
  `状態フラグ` varchar(500) DEFAULT NULL,
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_causes`
--

DROP TABLE IF EXISTS `tbl_gfa_causes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_causes` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT NULL COMMENT 'No',
  `cause` mediumtext COMMENT '原因',
  `cause_trans` mediumtext COMMENT '原因_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='GFA原因テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_causes_past`
--

DROP TABLE IF EXISTS `tbl_gfa_causes_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_causes_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `原因` mediumtext,
  `原因_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_conclusion`
--

DROP TABLE IF EXISTS `tbl_gfa_conclusion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_conclusion` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `conclusion` mediumtext COMMENT '結論',
  `conclusion_trans` mediumtext COMMENT '結論_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='GFA結論テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_conclusion_past`
--

DROP TABLE IF EXISTS `tbl_gfa_conclusion_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_conclusion_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `結論` mediumtext,
  `結論_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_examples`
--

DROP TABLE IF EXISTS `tbl_gfa_examples`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_examples` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `quantity` int DEFAULT '1' COMMENT '個数',
  `serial_num` varchar(120) DEFAULT NULL COMMENT 'No',
  `example` mediumtext COMMENT '事例',
  `example_trans` mediumtext COMMENT '事例_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` varchar(120) DEFAULT '0' COMMENT '部分翻訳',
  `val_comp` varchar(120) DEFAULT '0' COMMENT 'VAL完了',
  `val_comp_lock` varchar(120) DEFAULT '0' COMMENT 'VAL完了ロック',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='GFA事例テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_examples_past`
--

DROP TABLE IF EXISTS `tbl_gfa_examples_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_examples_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `個数` int DEFAULT '1',
  `No` varchar(120) DEFAULT '1',
  `事例` mediumtext,
  `事例_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` varchar(120) DEFAULT '0',
  `VAL完了` varchar(120) DEFAULT '0',
  `VAL完了ロック` varchar(120) DEFAULT '0',
  `SEQNo` varchar(160) DEFAULT '1',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_focusarea`
--

DROP TABLE IF EXISTS `tbl_gfa_focusarea`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_focusarea` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `focus_area` mediumtext COMMENT 'フォーカスエリア',
  `focus_area_trans` mediumtext COMMENT 'フォーカスエリア_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='GFAフォーカスエリアテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_focusarea_past`
--

DROP TABLE IF EXISTS `tbl_gfa_focusarea_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_focusarea_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `フォーカスエリア` mediumtext,
  `フォーカスエリア_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_insight`
--

DROP TABLE IF EXISTS `tbl_gfa_insight`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_insight` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `insight` mediumtext COMMENT '考察',
  `insight_trans` mediumtext COMMENT '考察_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='GFA考察テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_insight_past`
--

DROP TABLE IF EXISTS `tbl_gfa_insight_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_insight_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `考察` mediumtext,
  `考察_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

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
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_perspective`
--

DROP TABLE IF EXISTS `tbl_gfa_perspective`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_perspective` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `perspective` mediumtext COMMENT '取組状況',
  `perspective_trans` mediumtext COMMENT '取組状況_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='GFA取組状況テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_gfa_perspective_past`
--

DROP TABLE IF EXISTS `tbl_gfa_perspective_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_gfa_perspective_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `取組状況` mediumtext,
  `取組状況_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_itemmaster`
--

DROP TABLE IF EXISTS `tbl_itemmaster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_itemmaster` (
  `plant_name` varchar(10) NOT NULL COMMENT 'プラント名',
  `item` varchar(50) NOT NULL COMMENT '項目',
  `code` varchar(50) NOT NULL COMMENT 'コード',
  `name` varchar(50) DEFAULT NULL COMMENT '名称',
  `name_en` varchar(50) DEFAULT NULL COMMENT '名称（英語）',
  `division` varchar(10) DEFAULT NULL COMMENT '区分',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`plant_name`,`item`,`code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='アイテムマスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_labelmaster`
--

DROP TABLE IF EXISTS `tbl_labelmaster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_labelmaster` (
  `sheet_name` varchar(10) NOT NULL COMMENT 'シート名',
  `item` varchar(50) NOT NULL COMMENT '項目',
  `name` varchar(3000) DEFAULT NULL COMMENT '名称',
  `name_en` varchar(3000) DEFAULT NULL COMMENT '名称（英語）',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`sheet_name`,`item`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='ラベルマスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_number_control`
--

DROP TABLE IF EXISTS `tbl_number_control`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_number_control` (
  `plant_name` varchar(10) NOT NULL COMMENT 'プラント名',
  `kinds` varchar(3) NOT NULL COMMENT '種類',
  `fields` varchar(4) NOT NULL COMMENT '分野',
  `part_id` varchar(3) NOT NULL COMMENT '個別ID',
  `serial_num` int NOT NULL COMMENT '通番',
  `revisions` int NOT NULL COMMENT 'リビジョン',
  `language` varchar(1) DEFAULT NULL COMMENT '言語(NULL：日本語、E：英語)',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `num_no_revisions` varchar(50) DEFAULT NULL COMMENT '番号_リビジョンなし',
  `newest_flag` tinyint DEFAULT '1' COMMENT '最新フラグ(0：最新以外、1：最新)',
  `delete_flag` tinyint DEFAULT '0' COMMENT '削除フラグ(0：削除以外、1：削除)',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`revisions`,`serial_num`,`part_id`,`fields`,`kinds`,`plant_name`),
  KEY `ID_NUMBER_CONTROL_NUM_NO_REVERSION` (`num_no_revisions`),
  KEY `ID_NUMBER_CONTROL_FLAG` (`delete_flag`,`newest_flag`),
  KEY `ID_NUMBER_CONTROL_COMMON` (`plant_name`,`kinds`,`fields`,`part_id`,`delete_flag`,`newest_flag`) /*!80000 INVISIBLE */,
  KEY `ID_NUMBER_CONTROL_PLANT_NAME` (`plant_name`,`newest_flag`,`delete_flag`) /*!80000 INVISIBLE */,
  KEY `ID_NUMBER_CONTROL_NUM` (`num`) /*!80000 INVISIBLE */
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='番号を生成する';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_obs`
--

DROP TABLE IF EXISTS `tbl_obs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs` (
  `num` varchar(50) NOT NULL COMMENT '番号',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `title_trans` varchar(1000) DEFAULT NULL COMMENT 'タイトル_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `scope` mediumtext COMMENT '範囲',
  `scope_trans` mediumtext COMMENT '範囲_訳文',
  `scope_comment` mediumtext COMMENT '範囲_コメント',
  `part_trans_scope` tinyint DEFAULT '0' COMMENT '部分翻訳範囲',
  `observation_target` varchar(2) DEFAULT NULL COMMENT '観察対象',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
  `package_exclude` tinyint DEFAULT '0' COMMENT '最終パッケージに含めない',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了',
  `general_comment` varchar(1000) DEFAULT NULL COMMENT '全般コメント',
  `trans_range` tinyint DEFAULT '2' COMMENT '翻訳範囲(0：なし、1：部分、2：全文)',
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
  `hold` tinyint DEFAULT '1' COMMENT '開催中',
  `delete_flag` tinyint DEFAULT '0' COMMENT '削除フラグ(0：削除以外、1：削除)',
  `editor` varchar(3) DEFAULT NULL COMMENT '編集者',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBSテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`%`*/ /*!50003 TRIGGER `trg_insert_obs_number_control` BEFORE INSERT ON `tbl_obs` FOR EACH ROW BEGIN
		DECLARE plantName varchar(10);
		DECLARE kinds varchar(10);
		DECLARE fields varchar(10);
		DECLARE partId varchar(10);
		DECLARE serialNum int;
		DECLARE revisions int;
		DECLARE lang varchar(10);
        DECLARE referNum varchar(50);
		Set plantName = SUBSTRING_INDEX(NEW.num, '-', 1);
		Set kinds = SUBSTRING_INDEX(SUBSTRING_INDEX(NEW.num, '-', 2), '-', -1);
		Set fields = SUBSTRING_INDEX(SUBSTRING_INDEX(NEW.num, '-', 3), '-', -1);
		Set partId = SUBSTRING_INDEX(SUBSTRING_INDEX(NEW.num, '-', 4), '-', -1);
		Set serialNum = SUBSTRING_INDEX(SUBSTRING_INDEX(NEW.num, '-', 5), '-', -1);
		Set revisions = REPLACE(SUBSTRING_INDEX(SUBSTRING_INDEX(NEW.num, '-', 6), '-', -1), 'r', '');
        Set referNum = REPLACE(NEW.Num, CONCAT('-', SUBSTRING_INDEX(SUBSTRING_INDEX(NEW.num, '-', 6), '-', -1)), '');
		If((LENGTH(NEW.num) - LENGTH(REPLACE(NEW.num, '-', ''))) > 5) THEN
			Set lang = SUBSTRING_INDEX(SUBSTRING_INDEX(NEW.num, '-', 7), '-', -1);
		END IF;
		
		IF (SELECT count(num) FROM tbl_number_control WHERE num = NEW.num AND delete_flag = NEW.delete_flag) = 0
			THEN
				INSERT INTO `tbl_number_control` (`plant_name`, `kinds`, `fields`, `part_id`, `serial_num`, `revisions`, `language`, `num`, `num_no_revisions`, `newest_flag`, `delete_flag`, `last_user`)
				VALUES (plantName, kinds, fields, partId, serialNum, revisions, lang, NEW.num, referNum, 1, NEW.delete_flag, NEW.last_user);
		END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`%`*/ /*!50003 TRIGGER `trg_update_obs_number_control` AFTER UPDATE ON `tbl_obs` FOR EACH ROW BEGIN
	If(OLD.delete_flag <> NEW.delete_flag) THEN
		UPDATE `tbl_number_control`
		SET
			`delete_flag` = NEW.delete_flag,
			`last_user` = NEW.last_user
		WHERE (`num` = OLD.num) And (`delete_flag` = OLD.delete_flag);
	END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

--
-- Table structure for table `tbl_obs_attach`
--

DROP TABLE IF EXISTS `tbl_obs_attach`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs_attach` (
  `num_no_revisions` varchar(50) NOT NULL COMMENT '番号_リビジョンなし',
  `fact_num` int NOT NULL DEFAULT '1' COMMENT '観察事実番号(SEQNo)',
  `serial_num` int NOT NULL DEFAULT '1' COMMENT '連番',
  `represent_photo_flag` tinyint DEFAULT '0' COMMENT '代表写真フラグ(0：代表写真ではない、1：代表写真)',
  `file_name` text COMMENT 'ファイル名',
  `size` varchar(5) DEFAULT NULL COMMENT 'サイズ',
  `delete_flag` tinyint DEFAULT '0' COMMENT '削除フラグ(0：削除以外、1：削除)',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num_no_revisions`,`fact_num`,`serial_num`),
  KEY `ID_OBS_ATTACH_NUM_NO_VERSION` (`num_no_revisions`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PFA添付ファイル（写真）テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_obs_attach_past`
--

DROP TABLE IF EXISTS `tbl_obs_attach_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs_attach_past` (
  `num_no_revisions` varchar(50) NOT NULL COMMENT '番号_リビジョンなし',
  `fact_num` int NOT NULL DEFAULT '1' COMMENT '観察事実番号(SEQNo)',
  `serial_num` int NOT NULL DEFAULT '1' COMMENT '連番',
  `represent_photo_flag` tinyint DEFAULT '0' COMMENT '代表写真フラグ(0：代表写真ではない、1：代表写真)',
  `file_name` text COMMENT 'ファイル名',
  `size` varchar(5) DEFAULT NULL COMMENT 'サイズ',
  `delete_flag` tinyint DEFAULT '0' COMMENT '削除フラグ(0：削除以外、1：削除)',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num_no_revisions`,`fact_num`,`serial_num`),
  KEY `ID_OBS_ATTACH_PAST_NUM_NO_VERSION` (`num_no_revisions`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PFA添付ファイル（写真）テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_obs_conclusion`
--

DROP TABLE IF EXISTS `tbl_obs_conclusion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs_conclusion` (
  `num` varchar(50) NOT NULL COMMENT '番号',
  `conclusion_num` int NOT NULL DEFAULT '1' COMMENT '結論番号(SEQNo)',
  `conclusion` mediumtext COMMENT '結論',
  `conclusion_trans` mediumtext COMMENT '結論_訳文',
  `comment` mediumtext COMMENT 'コメント回答',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `connect_fact` varchar(240) DEFAULT NULL COMMENT '関連事実',
  `display_order` int DEFAULT NULL,
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num`,`conclusion_num`),
  KEY `fk_tbl_obs_conclusion_num` (`num`) /*!80000 INVISIBLE */
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBS結論テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_obs_conclusion_past`
--

DROP TABLE IF EXISTS `tbl_obs_conclusion_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs_conclusion_past` (
  `num` varchar(50) NOT NULL COMMENT '番号',
  `conclusion_num` int NOT NULL DEFAULT '1' COMMENT '結論番号(SEQNo)',
  `conclusion` mediumtext COMMENT '結論',
  `conclusion_trans` mediumtext COMMENT '結論_訳文',
  `comment` mediumtext COMMENT 'コメント回答',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `connect_fact` varchar(240) DEFAULT NULL COMMENT '関連事実',
  `display_order` int DEFAULT NULL,
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num`,`conclusion_num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBS結論テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_obs_fact`
--

DROP TABLE IF EXISTS `tbl_obs_fact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs_fact` (
  `num` varchar(50) NOT NULL COMMENT '番号',
  `fact_num` int NOT NULL DEFAULT '1' COMMENT '観察事実番号(SEQNo)',
  `fact` mediumtext COMMENT '事実',
  `fact_trans` mediumtext COMMENT '事実_訳文',
  `comment` mediumtext COMMENT 'コメント回答',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `plus_neutral_delta` tinyint DEFAULT '3' COMMENT 'PLUS_NEUTRAL_DELTA(1：Plus、2：Neutral、3：Delta)',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了(0：未完了、1：完了)',
  `offer_field` varchar(255) DEFAULT NULL COMMENT '提供先分野',
  `poc` varchar(255) DEFAULT NULL COMMENT 'POC',
  `safety_culture` varchar(255) DEFAULT NULL COMMENT '安全文化',
  `display_order` int DEFAULT NULL,
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num`,`fact_num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBC観察事実テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_obs_fact_past`
--

DROP TABLE IF EXISTS `tbl_obs_fact_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs_fact_past` (
  `num` varchar(50) NOT NULL COMMENT '番号',
  `fact_num` int NOT NULL DEFAULT '1' COMMENT '観察事実番号(SEQNo)',
  `fact` mediumtext COMMENT '事実',
  `fact_trans` mediumtext COMMENT '事実_訳文',
  `comment` mediumtext COMMENT 'コメント回答',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `plus_neutral_delta` tinyint DEFAULT '3' COMMENT 'PLUS_NEUTRAL_DELTA(1：Plus、2：Neutral、3：Delta)',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了(0：未完了、1：完了)',
  `offer_field` varchar(255) DEFAULT NULL COMMENT '提供先分野',
  `poc` varchar(255) DEFAULT NULL COMMENT 'POC',
  `safety_culture` varchar(255) DEFAULT NULL COMMENT '安全文化',
  `display_order` int DEFAULT NULL,
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num`,`fact_num`),
  KEY `fk_tbl_obs_past_idx` (`num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBC観察事実テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_obs_past`
--

DROP TABLE IF EXISTS `tbl_obs_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_obs_past` (
  `num` varchar(50) NOT NULL COMMENT '番号',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `title_trans` varchar(1000) DEFAULT NULL COMMENT 'タイトル_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `scope` mediumtext COMMENT '範囲',
  `scope_trans` mediumtext COMMENT '範囲_訳文',
  `scope_comment` mediumtext COMMENT '範囲_コメント',
  `part_trans_scope` tinyint DEFAULT '0' COMMENT '部分翻訳範囲',
  `observation_target` varchar(2) DEFAULT NULL COMMENT '観察対象',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
  `package_exclude` tinyint DEFAULT '0' COMMENT '最終パッケージに含めない',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了',
  `general_comment` varchar(1000) DEFAULT NULL COMMENT '全般コメント',
  `trans_range` tinyint DEFAULT '2' COMMENT '翻訳範囲(0：なし、1：部分、2：全文)',
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
  `hold` tinyint DEFAULT '1' COMMENT '開催中',
  `delete_flag` tinyint DEFAULT '0' COMMENT '削除フラグ(0：削除以外、1：削除)',
  `editor` varchar(3) DEFAULT NULL COMMENT '編集者',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='OBSテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pd`
--

DROP TABLE IF EXISTS `tbl_pd`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pd` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT NULL COMMENT 'プラント名',
  `years` int DEFAULT NULL COMMENT '年',
  `kinds` varchar(3) DEFAULT NULL COMMENT '種類',
  `fields` varchar(4) DEFAULT NULL COMMENT '分野',
  `part_id` varchar(3) DEFAULT NULL COMMENT '個別ID',
  `serial_num` int DEFAULT NULL COMMENT '通番',
  `revisions` int DEFAULT NULL COMMENT 'リビジョン',
  `achievement_goal` varchar(10) DEFAULT NULL COMMENT '達成目標',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
  `package_exclude` tinyint DEFAULT '0' COMMENT '最終パッケージに含めない',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了',
  `title` mediumtext COMMENT 'タイトル',
  `title_trans` mediumtext COMMENT 'タイトル_訳文',
  `title_part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳_タイトル',
  `explan_text` mediumtext COMMENT '説明文',
  `explan_text_trans` mediumtext COMMENT '説明文_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `explan_text_translation` tinyint DEFAULT '0' COMMENT '説明文_翻訳',
  `explan_text_jude` tinyint DEFAULT '0' COMMENT '説明文_審査',
  `explan_text_jdc` varchar(1000) DEFAULT NULL COMMENT '説明文_JDC',
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
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PDテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pd_attach`
--

DROP TABLE IF EXISTS `tbl_pd_attach`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pd_attach` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(5000) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(500) DEFAULT NULL COMMENT '連番',
  `seq_num` varchar(500) DEFAULT NULL COMMENT 'SEQNo',
  `represent_phot` varchar(500) DEFAULT NULL COMMENT '代表写真',
  `file_name` text COMMENT 'ファイル名',
  `size` varchar(1000) DEFAULT NULL COMMENT 'サイズ',
  `state_flag` varchar(500) DEFAULT NULL COMMENT '状態フラグ',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PD添付ファイル（写真）(写真)テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pd_attach_past`
--

DROP TABLE IF EXISTS `tbl_pd_attach_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pd_attach_past` (
  `num` varchar(50) NOT NULL COMMENT '番号',
  `serial_num` int NOT NULL DEFAULT '0' COMMENT '連番',
  `seq_num` int NOT NULL DEFAULT '0' COMMENT 'SEQNo',
  `represent_phot` tinyint DEFAULT NULL COMMENT '代表写真',
  `file_name` text COMMENT 'ファイル名',
  `size` varchar(1000) DEFAULT NULL COMMENT 'サイズ',
  `state_flag` tinyint DEFAULT NULL COMMENT '状態フラグ',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`num`,`serial_num`,`seq_num`),
  UNIQUE KEY `ID_UNIQUE` (`num`,`serial_num`,`seq_num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PFA添付ファイル（写真）テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pd_past`
--

DROP TABLE IF EXISTS `tbl_pd_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pd_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `プラント名` varchar(10) DEFAULT NULL,
  `年` int DEFAULT NULL,
  `種類` varchar(3) DEFAULT NULL,
  `分野` varchar(4) DEFAULT NULL,
  `個別ID` varchar(3) DEFAULT NULL,
  `通番` int DEFAULT NULL,
  `リビジョン` int DEFAULT NULL,
  `達成目標` varchar(10) DEFAULT NULL,
  `最終版とする` tinyint DEFAULT '0',
  `最終パッケージに含めない` tinyint DEFAULT '0',
  `VAL完了` tinyint DEFAULT '0',
  `タイトル` mediumtext,
  `タイトル_訳文` mediumtext,
  `部分翻訳_タイトル` tinyint DEFAULT '0',
  `説明文` mediumtext,
  `説明文_訳文` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `説明文_翻訳` tinyint DEFAULT '0',
  `説明文_審査` tinyint DEFAULT '0',
  `説明文_JDC` varchar(1000) DEFAULT NULL,
  `全般_JDC` varchar(1000) DEFAULT NULL,
  `関連番号` varchar(10) DEFAULT NULL,
  `関連メモ` varchar(30) DEFAULT NULL,
  `関連メモ_訳文` varchar(60) DEFAULT NULL,
  `関連メモ_部分翻訳` tinyint DEFAULT '0',
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
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_peerreview`
--

DROP TABLE IF EXISTS `tbl_peerreview`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_peerreview` (
  `years` int NOT NULL COMMENT '年度',
  `hold_num` int NOT NULL COMMENT '開催回',
  `review _name` varchar(50) NOT NULL COMMENT 'レビュー先',
  `plant_name` varchar(50) NOT NULL COMMENT 'プラント名',
  `hold` tinyint DEFAULT '0' COMMENT '開催中(''0：開催中以外、1：開催中)',
  `past_pr_subject` tinyint DEFAULT '0' COMMENT '過去PR対象(0：対象外、1：対象)',
  `dm_division` varchar(1) DEFAULT NULL COMMENT 'DM区分(D：電力、M：メーカー、F：フォローアップ)',
  `offer_division` varchar(1) DEFAULT NULL COMMENT '提供先区分(A：分野、P：PO＆C)',
  `sync_subject_live` tinyint DEFAULT '1' COMMENT '同期対象_ライブ',
  `sync_subject_past` tinyint DEFAULT '0' COMMENT '同期対象_過去',
  `follow_up` varchar(50) DEFAULT NULL COMMENT 'フォローアップ元',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`years`,`hold_num`,`review _name`,`plant_name`),
  KEY `ID_PEER_REVIEW_PLANT_NAME` (`plant_name`) /*!80000 INVISIBLE */,
  KEY `ID_PEER_REVIEW_HOLD` (`hold`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='ピアレビューテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa`
--

DROP TABLE IF EXISTS `tbl_pfa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT NULL COMMENT 'プラント名',
  `years` int DEFAULT NULL COMMENT '年',
  `kinds` varchar(3) DEFAULT NULL COMMENT '種類',
  `fields` varchar(4) DEFAULT NULL COMMENT '分野',
  `part_id` varchar(3) DEFAULT NULL COMMENT '個別ID',
  `branch_num` int DEFAULT NULL COMMENT '通番',
  `revisions` int DEFAULT NULL COMMENT 'リビジョン',
  `achievement_goal` varchar(10) DEFAULT NULL COMMENT '達成目標',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `title_trans` varchar(1000) DEFAULT NULL COMMENT 'タイトル_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `val_comp` tinyint DEFAULT '0' COMMENT 'VAL完了',
  `general_jdc` varchar(1000) DEFAULT NULL COMMENT '全般_JDC',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
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
  `delete_flag` tinyint NOT NULL DEFAULT '0' COMMENT '削除フラグ',
  `editor` varchar(3) DEFAULT NULL COMMENT '編集者',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) NOT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PFAテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa_attach_past`
--

DROP TABLE IF EXISTS `tbl_pfa_attach_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa_attach_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(5000) DEFAULT NULL,
  `連番` varchar(500) DEFAULT NULL,
  `SEQNo` varchar(500) DEFAULT NULL,
  `代表写真` varchar(500) DEFAULT NULL,
  `ファイル名` text,
  `サイズ` varchar(1000) DEFAULT NULL,
  `状態フラグ` varchar(500) DEFAULT NULL,
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa_conclusion`
--

DROP TABLE IF EXISTS `tbl_pfa_conclusion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa_conclusion` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `finding` mediumtext COMMENT '結論',
  `finding_trans` mediumtext COMMENT '結論_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PFA結論テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa_conclusion_past`
--

DROP TABLE IF EXISTS `tbl_pfa_conclusion_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa_conclusion_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `結論` mediumtext,
  `結論_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa_examples`
--

DROP TABLE IF EXISTS `tbl_pfa_examples`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa_examples` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `quantity` int DEFAULT '1' COMMENT '個数',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `example` mediumtext COMMENT '事例',
  `example_trans` mediumtext COMMENT '事例_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` varchar(120) DEFAULT '0' COMMENT '部分翻訳',
  `val_comp` varchar(120) DEFAULT '0' COMMENT 'VAL完了',
  `val_comp_lock` varchar(120) DEFAULT '0' COMMENT 'VAL完了ロック',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PFA事例テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa_examples_past`
--

DROP TABLE IF EXISTS `tbl_pfa_examples_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa_examples_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `個数` int DEFAULT '1',
  `No` varchar(120) DEFAULT '1',
  `事例` mediumtext,
  `事例_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` varchar(120) DEFAULT '0',
  `VAL完了` varchar(120) DEFAULT '0',
  `VAL完了ロック` varchar(120) DEFAULT '0',
  `SEQNo` varchar(160) DEFAULT '1',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa_focusarea`
--

DROP TABLE IF EXISTS `tbl_pfa_focusarea`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa_focusarea` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `focus_area` mediumtext COMMENT 'フォーカスエリア',
  `focus_area_trans` mediumtext COMMENT 'フォーカスエリア_訳文',
  `judge` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='PFAフォーカスエリアテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa_focusarea_past`
--

DROP TABLE IF EXISTS `tbl_pfa_focusarea_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa_focusarea_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `フォーカスエリア` mediumtext,
  `フォーカスエリア_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_pfa_past`
--

DROP TABLE IF EXISTS `tbl_pfa_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_pfa_past` (
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
  `タイトル_訳文` varchar(1000) DEFAULT NULL,
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
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_po&c`
--

DROP TABLE IF EXISTS `tbl_po&c`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_po&c` (
  `plant_name` varchar(50) NOT NULL COMMENT 'プラント名',
  `fields` varchar(255) NOT NULL COMMENT '分野',
  `po_c` varchar(255) NOT NULL COMMENT 'PO&C',
  `serial_num` int NOT NULL COMMENT 'No',
  `po_c_name` varchar(255) DEFAULT NULL COMMENT 'PO&C名称',
  `po_c_name_en` varchar(255) DEFAULT NULL COMMENT 'PO&C名称（英語）',
  `achievement_goal` varchar(255) DEFAULT NULL COMMENT '達成目標',
  `achievement_goal_en` varchar(1000) DEFAULT NULL COMMENT '達成目標（英語）',
  `display_order` int DEFAULT NULL COMMENT '表示順',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`plant_name`,`fields`,`po_c`,`serial_num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='達成目標（PO&C）マスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_reviewmember`
--

DROP TABLE IF EXISTS `tbl_reviewmember`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_reviewmember` (
  `plant_name` varchar(50) NOT NULL COMMENT 'プラント名',
  `initial` varchar(3) NOT NULL COMMENT 'イニシャル',
  `tl` tinyint DEFAULT '0' COMMENT 'TL',
  `judge` tinyint DEFAULT '0' COMMENT '審査者',
  `editor` tinyint DEFAULT '0' COMMENT '編集者',
  `trans` tinyint DEFAULT '0' COMMENT '翻訳者',
  `judge1` varchar(3) DEFAULT NULL COMMENT '承認者_審1',
  `judge2` varchar(3) DEFAULT NULL COMMENT '承認者_審2',
  `judge3` varchar(3) DEFAULT NULL COMMENT '承認者_審3',
  `tl_judge` varchar(3) DEFAULT NULL COMMENT '承認者_TL',
  `comment` varchar(255) DEFAULT NULL COMMENT 'コメント',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`plant_name`,`initial`),
  KEY `ID_REVIEW_MEMBER_INITIAL` (`initial`) /*!80000 INVISIBLE */
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='ピアレビューメンバマスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_safety_culture`
--

DROP TABLE IF EXISTS `tbl_safety_culture`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_safety_culture` (
  `plant_name` varchar(50) NOT NULL COMMENT 'プラント名',
  `safety_culture` varchar(255) NOT NULL COMMENT '安全文化',
  `serial_num` int NOT NULL COMMENT 'No',
  `safety_culture_name` varchar(255) DEFAULT NULL COMMENT '安全文化名称',
  `safety_culture_name_en` varchar(255) DEFAULT NULL COMMENT '安全文化名称（英語）',
  `achievement_goal` varchar(255) DEFAULT NULL COMMENT '達成目標',
  `achievement_goal_en` varchar(1000) DEFAULT NULL COMMENT '達成目標（英語）',
  `display_order` int DEFAULT NULL COMMENT '表示順',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`plant_name`,`safety_culture`,`serial_num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='安全文化マスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_sharedfolder`
--

DROP TABLE IF EXISTS `tbl_sharedfolder`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_sharedfolder` (
  `plant_name` varchar(50) NOT NULL COMMENT 'プラント名',
  `menu_pposi` int DEFAULT NULL COMMENT 'メニュー位置(NULLは、保存先フォルダ)',
  `disp_name` varchar(40) NOT NULL COMMENT '表示名',
  `disp_name_en` varchar(40) DEFAULT NULL COMMENT '表示名英',
  `path` varchar(255) DEFAULT NULL COMMENT 'パス',
  `valid` tinyint DEFAULT '0' COMMENT '有効',
  `valid_en` tinyint DEFAULT '0' COMMENT '有効英',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`plant_name`,`disp_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='共有フォルダ／保存先フォルダマスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi`
--

DROP TABLE IF EXISTS `tbl_soi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `plant_name` varchar(10) DEFAULT NULL COMMENT 'プラント名',
  `years` int DEFAULT NULL COMMENT '年',
  `kinds` varchar(3) DEFAULT NULL COMMENT '種類',
  `fields` varchar(4) DEFAULT NULL COMMENT '分野',
  `part_id` varchar(3) DEFAULT NULL COMMENT '個別ID',
  `branch_num` int DEFAULT NULL COMMENT '通番',
  `revisions` int DEFAULT NULL COMMENT 'リビジョン',
  `achievement_goal` varchar(10) DEFAULT NULL COMMENT '達成目標',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `title_trans` varchar(1000) DEFAULT NULL COMMENT 'タイトル_訳文',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `final_ver` tinyint DEFAULT '0' COMMENT '最終版とする',
  `package_exclude` tinyint DEFAULT '0' COMMENT '最終パッケージに含めない',
  `follow_up_num` varchar(50) DEFAULT NULL COMMENT 'フォローアップ元の番号',
  `general_jdc` varchar(1000) DEFAULT NULL COMMENT '全般_JDC',
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
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='SOIテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_currentstatus`
--

DROP TABLE IF EXISTS `tbl_soi_currentstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_currentstatus` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `current` mediumtext COMMENT '現在',
  `current_trans` mediumtext COMMENT '現在_訳文',
  `jude` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `result_division` varchar(50) DEFAULT NULL COMMENT '結果区分',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='SOI取組状況テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_currentstatus_past`
--

DROP TABLE IF EXISTS `tbl_soi_currentstatus_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_currentstatus_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `現在` mediumtext,
  `現在_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `結果区分` varchar(50) DEFAULT NULL,
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_example`
--

DROP TABLE IF EXISTS `tbl_soi_example`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_example` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `quantity` int DEFAULT '1' COMMENT '個数',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `example` mediumtext COMMENT '事例',
  `example_trans` mediumtext COMMENT '事例_訳文',
  `judg` mediumtext COMMENT '審査',
  `part_trans` varchar(120) DEFAULT '0' COMMENT '部分翻訳',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='SOI事例テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_example_past`
--

DROP TABLE IF EXISTS `tbl_soi_example_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_example_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `個数` int DEFAULT '1',
  `No` varchar(120) DEFAULT '1',
  `事例` mediumtext,
  `事例_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` varchar(120) DEFAULT '0',
  `SEQNo` varchar(160) DEFAULT '1',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_keycauses`
--

DROP TABLE IF EXISTS `tbl_soi_keycauses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_keycauses` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `major` mediumtext COMMENT '主要',
  `major_trans` mediumtext COMMENT '主要_訳文',
  `judg` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='SOI考察テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_keycauses_past`
--

DROP TABLE IF EXISTS `tbl_soi_keycauses_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_keycauses_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `主要` mediumtext,
  `主要_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_past`
--

DROP TABLE IF EXISTS `tbl_soi_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_past` (
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
  `タイトル_訳文` varchar(1000) DEFAULT NULL,
  `部分翻訳` tinyint DEFAULT '0',
  `最終版とする` tinyint DEFAULT '0',
  `最終パッケージに含めない` tinyint DEFAULT '0',
  `フォローアップ元の番号` varchar(50) DEFAULT NULL,
  `全般_JDC` varchar(1000) DEFAULT NULL,
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
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_performanceproblem`
--

DROP TABLE IF EXISTS `tbl_soi_performanceproblem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_performanceproblem` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `excellence` mediumtext COMMENT 'エクセレンス',
  `excellence_trans` mediumtext COMMENT 'エクセレンス_訳文',
  `jude` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='SOI結論テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_soi_performanceproblem_past`
--

DROP TABLE IF EXISTS `tbl_soi_performanceproblem_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_soi_performanceproblem_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `エクセレンス` mediumtext,
  `エクセレンス_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

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
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='STRテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_str_attach`
--

DROP TABLE IF EXISTS `tbl_str_attach`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_str_attach` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(5000) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(500) DEFAULT NULL COMMENT '連番',
  `seq_num` varchar(500) DEFAULT NULL COMMENT 'SEQNo',
  `represent_phot` varchar(500) DEFAULT NULL COMMENT '代表写真',
  `file_name` text COMMENT 'ファイル名',
  `size` varchar(1000) DEFAULT NULL COMMENT 'サイズ',
  `state_flag` varchar(500) DEFAULT NULL COMMENT '状態フラグ',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='STR添付ファイル（写真）テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_str_attach_past`
--

DROP TABLE IF EXISTS `tbl_str_attach_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_str_attach_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(5000) DEFAULT NULL,
  `連番` varchar(500) DEFAULT NULL,
  `SEQNo` varchar(500) DEFAULT NULL,
  `代表写真` varchar(500) DEFAULT NULL,
  `ファイル名` text,
  `サイズ` varchar(1000) DEFAULT NULL,
  `状態フラグ` varchar(500) DEFAULT NULL,
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_str_examples`
--

DROP TABLE IF EXISTS `tbl_str_examples`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_str_examples` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `quantity` int DEFAULT '1' COMMENT '個数',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `example` mediumtext COMMENT '観察事例',
  `example_trans` mediumtext COMMENT '観察事例_訳文',
  `jude` mediumtext COMMENT '審査',
  `part_trans` varchar(120) DEFAULT '0' COMMENT '部分翻訳',
  `val_comp` varchar(120) DEFAULT '0' COMMENT 'VAL完了',
  `val_comp_lock` varchar(120) DEFAULT '0' COMMENT 'VAL完了ロック',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='STR観察事例テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_str_examples_past`
--

DROP TABLE IF EXISTS `tbl_str_examples_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_str_examples_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `個数` int DEFAULT '1',
  `No` varchar(120) DEFAULT '1',
  `観察事例` mediumtext,
  `観察事例_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` varchar(120) DEFAULT '0',
  `VAL完了` varchar(120) DEFAULT '0',
  `VAL完了ロック` varchar(120) DEFAULT '0',
  `SEQNo` varchar(160) DEFAULT '1',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_str_past`
--

DROP TABLE IF EXISTS `tbl_str_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_str_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `プラント名` varchar(10) DEFAULT NULL,
  `年` int DEFAULT NULL,
  `種類` varchar(3) DEFAULT NULL,
  `分野` varchar(4) DEFAULT NULL,
  `個別ID` varchar(3) DEFAULT NULL,
  `通番` int DEFAULT NULL,
  `リビジョン` int DEFAULT NULL,
  `達成目標` varchar(10) DEFAULT NULL,
  `最終版とする` tinyint DEFAULT '0',
  `最終パッケージに含めない` tinyint DEFAULT '0',
  `VAL完了` tinyint DEFAULT '0',
  `タイトル` varchar(1000) DEFAULT NULL,
  `タイトル_訳文` varchar(1000) DEFAULT NULL,
  `部分翻訳` tinyint DEFAULT '0',
  `全般_JDC` varchar(1000) DEFAULT NULL,
  `関連番号` varchar(10) DEFAULT NULL,
  `関連メモ` varchar(30) DEFAULT NULL,
  `関連メモ_訳文` varchar(60) DEFAULT NULL,
  `関連メモ_部分翻訳` tinyint DEFAULT '0',
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
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_str_strength`
--

DROP TABLE IF EXISTS `tbl_str_strength`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_str_strength` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `strength` mediumtext COMMENT '良好事例',
  `strength_trans` mediumtext COMMENT '良好事例_訳文',
  `jude` mediumtext COMMENT '審査',
  `part_trans` tinyint DEFAULT '0' COMMENT '部分翻訳',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ID_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='STR良好事例テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_str_strength_past`
--

DROP TABLE IF EXISTS `tbl_str_strength_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_str_strength_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `番号` varchar(50) DEFAULT NULL,
  `No` int DEFAULT '1',
  `良好事例` mediumtext,
  `良好事例_訳文` mediumtext,
  `審査` mediumtext,
  `部分翻訳` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_synchpath`
--

DROP TABLE IF EXISTS `tbl_synchpath`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_synchpath` (
  `record_no` int NOT NULL AUTO_INCREMENT COMMENT 'レコード番号',
  `path_no` int unsigned DEFAULT NULL COMMENT 'パス番号',
  `synch_path` varchar(150) DEFAULT NULL COMMENT '同期先パス',
  `comment` varchar(30) DEFAULT NULL COMMENT 'コメント',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`record_no`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='同期先設定テーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_translate`
--

DROP TABLE IF EXISTS `tbl_translate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_translate` (
  `num` varchar(50) NOT NULL COMMENT '番号',
  `plant_name` varchar(10) DEFAULT NULL COMMENT 'プラント名',
  `offer_num` int unsigned DEFAULT NULL COMMENT '依頼番号',
  `title` mediumtext COMMENT 'タイトル',
  `title_trans` mediumtext COMMENT 'タイトル_訳文',
  `ver_trans_done` int DEFAULT NULL COMMENT '最新バージョン_翻訳済',
  `ver_original` int DEFAULT NULL COMMENT '最新バージョン_原文',
  `editor` varchar(3) DEFAULT NULL COMMENT '編集者',
  `trans_situation` varchar(10) DEFAULT NULL COMMENT '翻訳状況',
  `trans_arrival` varchar(1) DEFAULT NULL COMMENT '翻訳到着',
  `trans_cancel` varchar(3) DEFAULT NULL COMMENT '翻訳キャンセル',
  `trans_urgent` varchar(3) DEFAULT NULL COMMENT '翻訳至急',
  `contact_memo` varchar(255) DEFAULT NULL COMMENT '連絡メモ',
  `output_date` datetime DEFAULT NULL COMMENT '出力日',
  `capture_date` datetime DEFAULT NULL COMMENT '取込日',
  `trans_date` datetime DEFAULT NULL COMMENT '翻訳日',
  `translator` varchar(20) DEFAULT NULL COMMENT '翻訳者',
  `cancel_status` varchar(10) DEFAULT NULL COMMENT '取消状況(取消依頼中、翻訳継続、取消済)',
  `trans_scope` varchar(10) DEFAULT NULL COMMENT '翻訳範囲',
  `trans_deadline` varchar(30) DEFAULT NULL COMMENT '翻訳希望期限',
  `trans_lang` varchar(1) DEFAULT NULL COMMENT '翻訳言語',
  `trans_req_date` datetime DEFAULT NULL COMMENT '翻訳依頼日',
  `file_name` varchar(255) DEFAULT NULL COMMENT 'ファイル名',
  `select_check` tinyint DEFAULT '0' COMMENT '選択チェック',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`num`),
  KEY `ID_TRANSLATE_PLANT_NAME` (`plant_name`),
  KEY `ID_TRANSLATE_NUM` (`num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='WORDファイル格納情報ファイル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_user`
--

DROP TABLE IF EXISTS `tbl_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_user` (
  `initial` varchar(3) NOT NULL COMMENT 'イニシャル',
  `name` varchar(50) DEFAULT NULL COMMENT '氏名',
  `roma_name` varchar(50) DEFAULT NULL COMMENT 'ローマ字',
  `respns_area` varchar(4) DEFAULT NULL COMMENT '担当分野(分野マスタ)',
  `comment` varchar(255) DEFAULT NULL COMMENT 'コメント',
  `language` varchar(1) DEFAULT NULL COMMENT '言語(NULL：日本語、E：英語)',
  `coordinator` tinyint DEFAULT '0' COMMENT '調整員',
  `trans` tinyint DEFAULT '0' COMMENT '翻訳者',
  `valid` tinyint DEFAULT '0' COMMENT '有効',
  `pass` varchar(255) DEFAULT NULL COMMENT 'パスワード',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`initial`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='ユーザマスタ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_wc`
--

DROP TABLE IF EXISTS `tbl_wc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_wc` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `プラント名` varchar(10) DEFAULT NULL,
  `年` int DEFAULT NULL,
  `種類` varchar(3) DEFAULT NULL,
  `分野` varchar(4) DEFAULT NULL,
  `個別ID` varchar(3) DEFAULT NULL,
  `通番` int DEFAULT NULL,
  `リビジョン` int DEFAULT NULL,
  `英語版` varchar(1) DEFAULT NULL,
  `記述` text,
  `記述_訳文` text,
  `記述_JDC` text,
  `ユニットコード` varchar(1) DEFAULT '0',
  `ユニットその他` varchar(255) DEFAULT NULL,
  `ユニットその他_訳文` varchar(255) DEFAULT NULL,
  `建屋コード` varchar(1) DEFAULT '0',
  `建屋その他` varchar(255) DEFAULT NULL,
  `建屋その他_訳文` varchar(255) DEFAULT NULL,
  `階数場所` varchar(255) DEFAULT NULL,
  `階数場所_訳文` varchar(255) DEFAULT NULL,
  `分類コード` varchar(2) DEFAULT '0',
  `分類その他` varchar(255) DEFAULT NULL,
  `分類その他_訳文` varchar(255) DEFAULT NULL,
  `最終版とする` tinyint DEFAULT '0',
  `提供先分野1` tinyint DEFAULT '0',
  `提供先分野2` tinyint DEFAULT '0',
  `提供先分野3` tinyint DEFAULT '0',
  `提供先分野4` tinyint DEFAULT '0',
  `提供先分野5` tinyint DEFAULT '0',
  `提供先分野6` tinyint DEFAULT '0',
  `提供先分野7` tinyint DEFAULT '0',
  `提供先分野8` tinyint DEFAULT '0',
  `提供先分野9` tinyint DEFAULT '0',
  `提供先分野10` tinyint DEFAULT '0',
  `提供先分野11` tinyint DEFAULT '0',
  `PlusDelta` int unsigned DEFAULT '3',
  `翻訳範囲` varchar(10) DEFAULT '2',
  `翻訳希望期限` varchar(30) DEFAULT NULL,
  `翻訳言語` varchar(1) DEFAULT NULL,
  `翻訳状況依頼` tinyint DEFAULT '0',
  `翻訳状況依頼_個別ID` varchar(3) DEFAULT NULL,
  `翻訳依頼日` datetime DEFAULT NULL,
  `翻訳状況翻訳中` tinyint DEFAULT '0',
  `翻訳状況翻訳済` tinyint DEFAULT '0',
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
  `開催中` tinyint DEFAULT '-1',
  `最新フラグ` tinyint DEFAULT '-1',
  `削除フラグ` tinyint DEFAULT '0',
  `編集者` varchar(3) DEFAULT NULL,
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_wc_attach`
--

DROP TABLE IF EXISTS `tbl_wc_attach`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_wc_attach` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `プラント名` varchar(10) DEFAULT NULL,
  `種類` varchar(3) DEFAULT NULL,
  `分野` varchar(4) DEFAULT NULL,
  `個別ID` varchar(3) DEFAULT NULL,
  `通番` int DEFAULT NULL,
  `No` int unsigned DEFAULT NULL,
  `代表` tinyint DEFAULT '0',
  `ファイル名` varchar(255) DEFAULT NULL,
  `サイズ` int unsigned DEFAULT NULL,
  `最新フラグ` tinyint DEFAULT '-1',
  `削除フラグ` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_wc_attach_past`
--

DROP TABLE IF EXISTS `tbl_wc_attach_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_wc_attach_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `プラント名` varchar(10) DEFAULT NULL,
  `種類` varchar(3) DEFAULT NULL,
  `分野` varchar(4) DEFAULT NULL,
  `個別ID` varchar(3) DEFAULT NULL,
  `通番` int DEFAULT NULL,
  `No` int unsigned DEFAULT NULL,
  `代表` tinyint DEFAULT '0',
  `ファイル名` varchar(255) DEFAULT NULL,
  `サイズ` int unsigned DEFAULT NULL,
  `最新フラグ` tinyint DEFAULT '-1',
  `削除フラグ` tinyint DEFAULT '0',
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_wc_past`
--

DROP TABLE IF EXISTS `tbl_wc_past`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_wc_past` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `プラント名` varchar(10) DEFAULT NULL,
  `年` int DEFAULT NULL,
  `種類` varchar(3) DEFAULT NULL,
  `分野` varchar(4) DEFAULT NULL,
  `個別ID` varchar(3) DEFAULT NULL,
  `通番` int DEFAULT NULL,
  `リビジョン` int DEFAULT NULL,
  `英語版` varchar(1) DEFAULT NULL,
  `記述` text,
  `記述_訳文` text,
  `記述_JDC` text,
  `ユニットコード` varchar(1) DEFAULT '0',
  `ユニットその他` varchar(255) DEFAULT NULL,
  `ユニットその他_訳文` varchar(255) DEFAULT NULL,
  `建屋コード` varchar(1) DEFAULT '0',
  `建屋その他` varchar(255) DEFAULT NULL,
  `建屋その他_訳文` varchar(255) DEFAULT NULL,
  `階数場所` varchar(255) DEFAULT NULL,
  `階数場所_訳文` varchar(255) DEFAULT NULL,
  `分類コード` varchar(2) DEFAULT '0',
  `分類その他` varchar(255) DEFAULT NULL,
  `分類その他_訳文` varchar(255) DEFAULT NULL,
  `最終版とする` tinyint DEFAULT '0',
  `提供先分野1` tinyint DEFAULT '0',
  `提供先分野2` tinyint DEFAULT '0',
  `提供先分野3` tinyint DEFAULT '0',
  `提供先分野4` tinyint DEFAULT '0',
  `提供先分野5` tinyint DEFAULT '0',
  `提供先分野6` tinyint DEFAULT '0',
  `提供先分野7` tinyint DEFAULT '0',
  `提供先分野8` tinyint DEFAULT '0',
  `提供先分野9` tinyint DEFAULT '0',
  `提供先分野10` tinyint DEFAULT '0',
  `提供先分野11` tinyint DEFAULT '0',
  `PlusDelta` int unsigned DEFAULT '3',
  `翻訳範囲` varchar(10) DEFAULT '2',
  `翻訳希望期限` varchar(30) DEFAULT NULL,
  `翻訳言語` varchar(1) DEFAULT NULL,
  `翻訳状況依頼` tinyint DEFAULT '0',
  `翻訳状況依頼_個別ID` varchar(3) DEFAULT NULL,
  `翻訳依頼日` datetime DEFAULT NULL,
  `翻訳状況翻訳中` tinyint DEFAULT '0',
  `翻訳状況翻訳済` tinyint DEFAULT '0',
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
  `開催中` tinyint DEFAULT '-1',
  `最新フラグ` tinyint DEFAULT '-1',
  `削除フラグ` tinyint DEFAULT '0',
  `編集者` varchar(3) DEFAULT NULL,
  `最終更新日` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `最終更新者` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_wk_synchpath`
--

DROP TABLE IF EXISTS `tbl_wk_synchpath`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_wk_synchpath` (
  `storage_location` tinytext NOT NULL COMMENT '保管先',
  `last_update` datetime DEFAULT NULL COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`storage_location`(50))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='同期先パス格納ワークテーブル';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tbl_wk_wordlist`
--

DROP TABLE IF EXISTS `tbl_wk_wordlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_wk_wordlist` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `Num` varchar(45) DEFAULT NULL COMMENT '番号',
  `fields` varchar(4) DEFAULT NULL COMMENT '分野',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `newest_flag` tinyint(1) DEFAULT NULL COMMENT '最新フラグ',
  `delete_flag` tinyint(1) DEFAULT NULL COMMENT '削除フラグ',
  `print_order` int DEFAULT NULL COMMENT '印刷順序',
  `pre_change_order` int DEFAULT NULL COMMENT '変更前順序',
  `after_change_order` int DEFAULT NULL COMMENT '変更後順序',
  `output_target` tinyint(1) DEFAULT NULL COMMENT '出力対象',
  `package_exclude` tinyint(1) DEFAULT NULL COMMENT '最終パッケージに含めない',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='TblWkWordList';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `vw_obs_control`
--

DROP TABLE IF EXISTS `vw_obs_control`;
/*!50001 DROP VIEW IF EXISTS `vw_obs_control`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_obs_control` AS SELECT 
 1 AS `num`,
 1 AS `plant_name`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `serial_num`,
 1 AS `revisions`,
 1 AS `num_no_revisions`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `scope`,
 1 AS `scope_trans`,
 1 AS `scope_comment`,
 1 AS `part_trans_scope`,
 1 AS `observation_target`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `val_comp`,
 1 AS `general_comment`,
 1 AS `trans_range`,
 1 AS `trans_deadline`,
 1 AS `trans_lang`,
 1 AS `trans_state_req`,
 1 AS `trans_state_req_id`,
 1 AS `trans_req_date`,
 1 AS `translating_state`,
 1 AS `translating_state_id`,
 1 AS `translated_state`,
 1 AS `translated_state_id`,
 1 AS `approval_state_pr`,
 1 AS `approval_state_pr_id`,
 1 AS `approval_state_pr_rev`,
 1 AS `approval_state_pr_state`,
 1 AS `approval_state1`,
 1 AS `approval_state1_id`,
 1 AS `approval_state1_rev`,
 1 AS `approval_state2`,
 1 AS `approval_state2_id`,
 1 AS `approval_state2_rev`,
 1 AS `approval_state3`,
 1 AS `approval_state3_id`,
 1 AS `approval_state3_rev`,
 1 AS `approval_state_tl`,
 1 AS `approval_state_tl_id`,
 1 AS `approval_state_tl_rev`,
 1 AS `approval_state_tl_state`,
 1 AS `language`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_obs_extend`
--

DROP TABLE IF EXISTS `vw_obs_extend`;
/*!50001 DROP VIEW IF EXISTS `vw_obs_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_obs_extend` AS SELECT 
 1 AS `num`,
 1 AS `plant_name`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `serial_num`,
 1 AS `revisions`,
 1 AS `num_no_revisions`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `scope`,
 1 AS `scope_trans`,
 1 AS `scope_comment`,
 1 AS `part_trans_scope`,
 1 AS `observation_target`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `val_comp`,
 1 AS `general_comment`,
 1 AS `trans_range`,
 1 AS `trans_deadline`,
 1 AS `trans_lang`,
 1 AS `trans_state_req`,
 1 AS `trans_state_req_id`,
 1 AS `trans_req_date`,
 1 AS `translating_state`,
 1 AS `translating_state_id`,
 1 AS `translated_state`,
 1 AS `translated_state_id`,
 1 AS `approval_state_pr`,
 1 AS `approval_state_pr_id`,
 1 AS `approval_state_pr_rev`,
 1 AS `approval_state_pr_state`,
 1 AS `approval_state1`,
 1 AS `approval_state1_id`,
 1 AS `approval_state1_rev`,
 1 AS `approval_state2`,
 1 AS `approval_state2_id`,
 1 AS `approval_state2_rev`,
 1 AS `approval_state3`,
 1 AS `approval_state3_id`,
 1 AS `approval_state3_rev`,
 1 AS `approval_state_tl`,
 1 AS `approval_state_tl_id`,
 1 AS `approval_state_tl_rev`,
 1 AS `approval_state_tl_state`,
 1 AS `language`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cancel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_obs_extend_filter`
--

DROP TABLE IF EXISTS `vw_obs_extend_filter`;
/*!50001 DROP VIEW IF EXISTS `vw_obs_extend_filter`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_obs_extend_filter` AS SELECT 
 1 AS `num`,
 1 AS `plant_name`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `serial_num`,
 1 AS `revisions`,
 1 AS `num_no_revisions`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `scope`,
 1 AS `scope_trans`,
 1 AS `scope_comment`,
 1 AS `part_trans_scope`,
 1 AS `observation_target`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `val_comp`,
 1 AS `general_comment`,
 1 AS `trans_range`,
 1 AS `trans_deadline`,
 1 AS `trans_lang`,
 1 AS `trans_state_req`,
 1 AS `trans_state_req_id`,
 1 AS `trans_req_date`,
 1 AS `translating_state`,
 1 AS `translating_state_id`,
 1 AS `translated_state`,
 1 AS `translated_state_id`,
 1 AS `approval_state_pr`,
 1 AS `approval_state_pr_id`,
 1 AS `approval_state_pr_rev`,
 1 AS `approval_state_pr_state`,
 1 AS `approval_state1`,
 1 AS `approval_state1_id`,
 1 AS `approval_state1_rev`,
 1 AS `approval_state2`,
 1 AS `approval_state2_id`,
 1 AS `approval_state2_rev`,
 1 AS `approval_state3`,
 1 AS `approval_state3_id`,
 1 AS `approval_state3_rev`,
 1 AS `approval_state_tl`,
 1 AS `approval_state_tl_id`,
 1 AS `approval_state_tl_rev`,
 1 AS `approval_state_tl_state`,
 1 AS `language`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cancel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_obs_extend_refer`
--

DROP TABLE IF EXISTS `vw_obs_extend_refer`;
/*!50001 DROP VIEW IF EXISTS `vw_obs_extend_refer`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_obs_extend_refer` AS SELECT 
 1 AS `num`,
 1 AS `plant_name`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `serial_num`,
 1 AS `revisions`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `scope`,
 1 AS `scope_trans`,
 1 AS `scope_comment`,
 1 AS `part_trans_scope`,
 1 AS `observation_target`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `val_comp`,
 1 AS `general_comment`,
 1 AS `trans_range`,
 1 AS `trans_deadline`,
 1 AS `trans_lang`,
 1 AS `trans_state_req`,
 1 AS `trans_state_req_id`,
 1 AS `trans_req_date`,
 1 AS `translating_state`,
 1 AS `translating_state_id`,
 1 AS `translated_state`,
 1 AS `translated_state_id`,
 1 AS `approval_state_pr`,
 1 AS `approval_state_pr_id`,
 1 AS `approval_state_pr_rev`,
 1 AS `approval_state_pr_state`,
 1 AS `approval_state1`,
 1 AS `approval_state1_id`,
 1 AS `approval_state1_rev`,
 1 AS `approval_state2`,
 1 AS `approval_state2_id`,
 1 AS `approval_state2_rev`,
 1 AS `approval_state3`,
 1 AS `approval_state3_id`,
 1 AS `approval_state3_rev`,
 1 AS `approval_state_tl`,
 1 AS `approval_state_tl_id`,
 1 AS `approval_state_tl_rev`,
 1 AS `approval_state_tl_state`,
 1 AS `language`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `approval_status_tl_jpa`,
 1 AS `approval_status_tl_eng`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cancel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_obs_fact_attach`
--

DROP TABLE IF EXISTS `vw_obs_fact_attach`;
/*!50001 DROP VIEW IF EXISTS `vw_obs_fact_attach`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_obs_fact_attach` AS SELECT 
 1 AS `num`,
 1 AS `fact`,
 1 AS `fact_trans`,
 1 AS `comment`,
 1 AS `part_trans`,
 1 AS `plus_neutral_delta`,
 1 AS `val_comp`,
 1 AS `offer_field`,
 1 AS `poc`,
 1 AS `safety_culture`,
 1 AS `serial_num`,
 1 AS `num_no_revisions`,
 1 AS `fact_num`,
 1 AS `attach_fact_num`,
 1 AS `represent_photo_flag`,
 1 AS `file_name`,
 1 AS `size`,
 1 AS `delete_flag`,
 1 AS `display_order`,
 1 AS `last_update`,
 1 AS `last_user`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_translate_control`
--

DROP TABLE IF EXISTS `vw_translate_control`;
/*!50001 DROP VIEW IF EXISTS `vw_translate_control`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_translate_control` AS SELECT 
 1 AS `plant_name`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `serial_num`,
 1 AS `revisions`,
 1 AS `language`,
 1 AS `num`,
 1 AS `offer_num`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `ver_trans_done`,
 1 AS `ver_original`,
 1 AS `editor`,
 1 AS `trans_situation`,
 1 AS `trans_arrival`,
 1 AS `trans_cancel`,
 1 AS `trans_urgent`,
 1 AS `contact_memo`,
 1 AS `output_date`,
 1 AS `capture_date`,
 1 AS `trans_date`,
 1 AS `translator`,
 1 AS `cancel_status`,
 1 AS `trans_scope`,
 1 AS `trans_deadline`,
 1 AS `trans_lang`,
 1 AS `trans_req_date`,
 1 AS `file_name`,
 1 AS `select_check`,
 1 AS `last_update`,
 1 AS `last_user`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `wkt_afi_example`
--

DROP TABLE IF EXISTS `wkt_afi_example`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_afi_example` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `example` mediumtext COMMENT '事例',
  `example_trans` mediumtext COMMENT '事例_訳文',
  `judge` mediumtext COMMENT '審査',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_afi_example';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_analysis_afi_example`
--

DROP TABLE IF EXISTS `wkt_analysis_afi_example`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_analysis_afi_example` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `example` mediumtext COMMENT '事例',
  `example_trans` mediumtext COMMENT '事例_訳文',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_Analysis_AFI_Example';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_analysis_gfa_examples`
--

DROP TABLE IF EXISTS `wkt_analysis_gfa_examples`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_analysis_gfa_examples` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(120) DEFAULT NULL COMMENT 'No',
  `example` mediumtext COMMENT '事例',
  `example_trans` mediumtext COMMENT '事例_訳文',
  `judge` mediumtext COMMENT '審査',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_Analysis_GFA_Examples';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_analysis_obs`
--

DROP TABLE IF EXISTS `wkt_analysis_obs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_analysis_obs` (
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
  `field_order` int DEFAULT NULL COMMENT '分野並び',
  `copy_flg` tinyint DEFAULT NULL COMMENT 'コピーフラグ',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_Analysis_OBS';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_analysis_obs_conclusion`
--

DROP TABLE IF EXISTS `wkt_analysis_obs_conclusion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_analysis_obs_conclusion` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `conclusion` mediumtext COMMENT '結論',
  `conclusion_trans` mediumtext COMMENT '結論_訳文',
  `judge` mediumtext COMMENT '審査',
  `field_order` int DEFAULT NULL COMMENT '分野並び',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_Analysis_OBS_Conclusion';
/*!40101 SET character_set_client = @saved_cs_client */;

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
  `list_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_Analysis_WC';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_errordata`
--

DROP TABLE IF EXISTS `wkt_errordata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_errordata` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `kinds` varchar(3) DEFAULT '' COMMENT '種類',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `title` varchar(1000) DEFAULT NULL COMMENT 'タイトル',
  `title_trans` varchar(1000) DEFAULT NULL COMMENT 'タイトル_訳文',
  `output_order` int DEFAULT NULL COMMENT '出力順',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_ErrorData';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_gfa_examples`
--

DROP TABLE IF EXISTS `wkt_gfa_examples`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_gfa_examples` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_GFA_Examples';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_obs_conclusion`
--

DROP TABLE IF EXISTS `wkt_obs_conclusion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_obs_conclusion` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `conclusion` mediumtext COMMENT '結論',
  `conclusion_trans` mediumtext COMMENT '結論_訳文',
  `comment` mediumtext COMMENT '審査',
  `connect_fact` varchar(240) DEFAULT '0' COMMENT '関連事実',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_OBS_Conclusion';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_obs_fact`
--

DROP TABLE IF EXISTS `wkt_obs_fact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_obs_fact` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` int DEFAULT '1' COMMENT 'No',
  `fact` mediumtext COMMENT '事実',
  `fact_trans` mediumtext COMMENT '事実_訳文',
  `judge` mediumtext COMMENT '審査',
  `plus_neutral_delta` int DEFAULT '3' COMMENT 'PLUS_NEUTRAL_DELTA',
  `seq_num` int DEFAULT '1' COMMENT 'SEQNo',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_OBS_Fact';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_pfa_examples`
--

DROP TABLE IF EXISTS `wkt_pfa_examples`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_pfa_examples` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT NULL COMMENT '番号',
  `serial_num` varchar(120) DEFAULT '1' COMMENT 'No',
  `example` mediumtext COMMENT '事例',
  `example_trans` mediumtext COMMENT '事例_訳文',
  `judge` mediumtext COMMENT '審査',
  `seq_num` varchar(160) DEFAULT '1' COMMENT 'SEQNo',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_PFA_Examples';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_soi_example`
--

DROP TABLE IF EXISTS `wkt_soi_example`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_soi_example` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_SOI_Example';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_str_examples`
--

DROP TABLE IF EXISTS `wkt_str_examples`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_str_examples` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_STR_Examples';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_transhistory`
--

DROP TABLE IF EXISTS `wkt_transhistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_transhistory` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `item` varchar(50) DEFAULT NULL COMMENT '項目',
  `revisions` int DEFAULT '0' COMMENT 'リビジョン',
  `japanese` mediumtext COMMENT '和文',
  `english` mediumtext COMMENT '英文',
  `translated` varchar(100) DEFAULT '0' COMMENT '翻訳済',
  `display_order` int DEFAULT '0' COMMENT '表示順',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_TransHistory';
/*!40101 SET character_set_client = @saved_cs_client */;

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
  `trans_cancel` varchar(3) DEFAULT NULL COMMENT '翻訳キャンセル',
  `trans_urgent` varchar(3) DEFAULT NULL COMMENT '翻訳至急',
  `contact_memo` varchar(255) DEFAULT NULL COMMENT '連絡メモ',
  `output_date` datetime DEFAULT NULL COMMENT '出力日',
  `capture_date` datetime DEFAULT NULL COMMENT '取込日',
  `trans_date` datetime DEFAULT NULL COMMENT '翻訳日',
  `translator` varchar(20) DEFAULT NULL COMMENT '翻訳者',
  `cansel_status` varchar(10) DEFAULT NULL COMMENT '取消状況',
  `selection_cancel_status` varchar(10) DEFAULT NULL COMMENT '取消状況選択用',
  `trans_scope` varchar(10) DEFAULT NULL COMMENT '翻訳範囲',
  `trans_deadline` varchar(30) DEFAULT NULL COMMENT '翻訳希望期限',
  `trans_lang` varchar(1) DEFAULT NULL COMMENT '翻訳言語',
  `trans_req_date` datetime DEFAULT NULL COMMENT '翻訳依頼日',
  `file_name` varchar(255) DEFAULT NULL COMMENT 'ファイル名',
  `select_check` tinyint DEFAULT '0' COMMENT '選択チェック',
  `last_update` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最終更新日',
  `last_user` varchar(3) DEFAULT NULL COMMENT '最終更新者',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_translate';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `wkt_wclist`
--

DROP TABLE IF EXISTS `wkt_wclist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wkt_wclist` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `num` varchar(50) DEFAULT '' COMMENT '番号',
  `field` varchar(4) DEFAULT '' COMMENT '分野',
  `Description` mediumtext COMMENT '記述',
  `newest_flag` tinyint DEFAULT '-1' COMMENT '最新フラグ',
  `delete_flag` tinyint DEFAULT '0' COMMENT '削除フラグ',
  `print_order` int DEFAULT '0' COMMENT '印刷順序',
  `pre_change_order` int DEFAULT '0' COMMENT '変更前順序',
  `change_order` int DEFAULT '0' COMMENT '変更後順序',
  `output_target` int DEFAULT '0' COMMENT '出力対象',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='wkt_WCList';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping routines(Store procedure, function)
--
/*!50003 DROP FUNCTION IF EXISTS `Fn_Get_Constant` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fn_Get_Constant`(class_name varchar(255), constant_name varchar(255)) RETURNS varchar(255) CHARSET utf8mb4
    DETERMINISTIC
BEGIN

CASE class_name
 -- Language 
   WHEN 'LANGUAGE' THEN 
		CASE constant_name
			WHEN 'EN' THEN 
			return 'E';
		ELSE 
			return '';
		END CASE;
-- LanguageNumber 
   WHEN 'LANG_NUM' THEN 
		CASE constant_name
			WHEN 'JA' THEN 
				return '1';
            WHEN 'EN' THEN 
				return '2';
		ELSE 
			return '0'; -- origin
		END CASE;
 -- DELETE_FLAG 
	 WHEN 'DELETE_FLAG' THEN 
		CASE constant_name
			WHEN 'ON' THEN 
				return '1';
		ELSE 
			return '0';
		END CASE;
 -- NEWEST_FLAG 
	 WHEN 'NEWEST_FLAG' THEN 
		CASE constant_name
			WHEN 'ON' THEN 
				return '1';
		ELSE 
			return '0';
		END CASE;
-- TRANS_RANGE 
	 WHEN 'TRANS_RANGE' THEN 
		CASE constant_name
			WHEN 'NONE' THEN 
				return '0';
			WHEN 'PARTIAL' THEN 
				return '1';
		ELSE 
			return '2'; -- WHOLE
		END CASE;
-- HOLD 
	 WHEN 'HOLD_FLAG' THEN 
		CASE constant_name
			WHEN 'ON' THEN 
				return '1';
		ELSE 
			return '0'; -- OFF
		END CASE;
-- TRANSLATED_STATE
	WHEN 'TRANSLATED_STATE' THEN 
		CASE constant_name
			WHEN 'ON' THEN 
				return '1';
		ELSE 
			return '0'; -- NONE
		END CASE;
-- PART_TRANS
	WHEN 'PART_TRANS' THEN 
		CASE constant_name
			WHEN 'ON' THEN 
				return '1';
		ELSE 
			return '0'; -- NONE
		END CASE;
	ELSE  -- if don't have class name
		CASE constant_name
			WHEN 'EN' THEN 
			return 'E';
		ELSE 
			return '';
		END CASE;
        
 END CASE;
 
 
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `Fn_Get_Dm_DivisionF` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fn_Get_Dm_DivisionF`(in_PlantName varchar(50)) RETURNS varchar(1) CHARSET utf8mb4
    DETERMINISTIC
BEGIN
-- G_getDM区分F of Module2
	declare strDmDivision varchar(1);
    set strDmDivision =(SELECT tbl_PeerReview_1.dm_division 
						FROM tbl_PeerReview
                        LEFT JOIN tbl_PeerReview AS tbl_PeerReview_1
                        ON tbl_PeerReview.follow_up = tbl_PeerReview_1.plant_name
						WHERE
							tbl_PeerReview.plant_name = in_PlantName
						limit 1 );
RETURN strDmDivision;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `Fn_Get_Max_No` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fn_Get_Max_No`(plantName varchar(50), kind varchar(5), field varchar(30), dmDivision varchar(1), partId varchar(3)) RETURNS int
    DETERMINISTIC
BEGIN
	DECLARE maxId INT;
		IF(dmDivision = "F") THEN
			SET field := "FU";
		END IF;

		SET maxId = (SELECT COALESCE(MAX(tbl_number_control.serial_num),0) + 1 AS serial_num 
					FROM tbl_obs, tbl_number_control
					WHERE tbl_number_control.plant_name = plantName and tbl_obs.num = tbl_number_control.num
					AND
					   tbl_number_control.kinds = kind
					AND
					   tbl_number_control.fields = field
					AND
					   tbl_number_control.part_id = partId);
	RETURN maxId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `Fn_Get_Max_Offer_Num_Tbl_Tran` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fn_Get_Max_Offer_Num_Tbl_Tran`(plantName varchar(50)) RETURNS int
    DETERMINISTIC
BEGIN
	DECLARE maxId INT;
	SET maxId = (SELECT COALESCE(MAX(offer_num),0) + 1 AS offer_num 
				 FROM vw_translate_control
				 WHERE plant_name =  plantName
				);
	RETURN maxId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `Fn_Jap_Eng_Con_App_Status` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fn_Jap_Eng_Con_App_Status`(pLang varchar(1), pComment varchar(5000)) RETURNS varchar(5000) CHARSET utf8mb4
    DETERMINISTIC
BEGIN
DECLARE result varchar(5000);
	IF pLang = "E" THEN
        Case pComment
			WHEN "確認依頼" THEN
				SET result := "Check REQ";
			WHEN "上申" THEN
				SET result := "Report";
			WHEN "レビューワーに戻す" THEN
				SET result := "Returns to Reviewer";
			WHEN "承認" then
				SET result := "Recognition";
			WHEN "承認（コメント有）" THEN
				SET result := "Recognition(Comm)";
			WHEN "要修正" then
				SET result := "Correction Required";
			ELSE
				SET result := pComment;
        End case;
    ELSE
        CASE pComment
			WHEN "Check REQ" THEN
				SET result := "確認依頼" ;
			WHEN "Report" THEN
				SET result := "上申";
			WHEN "Returns to Reviewer" THEN
				SET result := "レビューワーに戻す";
			WHEN "Recognition" THEN
				SET result := "承認";
			WHEN "Recognition(Comm)" THEN
				SET result := "承認（コメント有）";
			WHEN "Correction Required" THEN
				SET result := "要修正";
			ELSE
				SET result := pComment;
        END CASE;
    END IF;
	RETURN result;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Creat_New_Obs` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Creat_New_Obs`(in_PlantName VARCHAR(50),
													   in_DmDivision VARCHAR(1),
													   in_PartId VARCHAR(3),
													   in_Area VARCHAR(30),
													   in_Lang VARCHAR(1)
                                                       )
BEGIN
	DECLARE intMaxNo INT;
    DECLARE sqlStr TEXT;
	DECLARE num varchar(100);
	-- EXIT HANDLER エラー メッセージを取得する
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    
	BEGIN
		GET DIAGNOSTICS CONDITION 1 @ERRNO = MYSQL_ERRNO, @MESSAGE_TEXT = MESSAGE_TEXT;
		SELECT 'ERROR' AS STATUS, CONCAT('MySQL ERROR: ', @ERRNO, ': ', @MESSAGE_TEXT) AS MESSAGE;
        ROLLBACK;
	END;
    
    START TRANSACTION;
    -- 最大の数値を取得します
    SET intMaxNo = (select Fn_Get_Max_No(in_PlantName,'OBS', in_Area, in_DmDivision, in_PartId));
    SET num = CONCAT( in_PlantName , "-" , 'OBS' , "-" , IF(in_DmDivision = "F", "FU", in_Area) , "-" , in_PartId , "-" , LPAD(intMaxNo,2, "0") , "-" , "r0", IF(in_Lang = "E", "-E", ""));
    
	-- OBSを挿入
    SET sqlStr := "INSERT INTO tbl_obs";
    SET sqlStr := CONCAT(sqlStr , "( num"); -- num
    SET sqlStr := CONCAT(sqlStr , ", editor"); -- 編集者
    SET sqlStr := CONCAT(sqlStr , ", last_user"); -- 最終更新者
    SET sqlStr := CONCAT(sqlStr , ") VALUES");
    SET sqlStr := CONCAT(sqlStr , " (? "); -- num
    SET sqlStr := CONCAT(sqlStr ,", ? "); -- partId (editor)
    SET sqlStr := CONCAT(sqlStr , ", ? "); -- partId (last_user)
    SET sqlStr := CONCAT(sqlStr , ");");
    
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @num = num;
    SET @editor_val = in_PartId;
    SET @last_user_val = in_PartId;
	EXECUTE dynamic_statement USING @num, @editor_val, @last_user_val;
    DEALLOCATE PREPARE dynamic_statement;
    
	-- OBSFACTを挿入
    SET sqlStr := '';   
    SET sqlStr := CONCAT(sqlStr, " INSERT INTO tbl_obs_fact (num, last_user) VALUES ( ? , ?); ");
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @num_val = num;
    SET @last_user_val = in_PartId;
    EXECUTE dynamic_statement USING @num_val, @last_user_val;
    DEALLOCATE PREPARE dynamic_statement;
	
	-- OBSConclusionを挿入
    SET sqlStr := '';   
    SET sqlStr := CONCAT(sqlStr, " INSERT INTO tbl_obs_conclusion (num, last_user) VALUES ( ? , ? ); ");
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @num_val = num;
    SET @last_user_val = in_PartId;
    EXECUTE dynamic_statement USING @num_val, @last_user_val;
    DEALLOCATE PREPARE dynamic_statement;

	COMMIT;    
    SET sqlStr := '';   
	-- 新しく作成されたレコードを取得します。
	SET sqlStr := CONCAT(" SELECT * FROM tbl_obs WHERE num = ?;");  
	SET @sqlexc :=sqlStr;
	PREPARE dynamic_statement FROM @sqlexc;
	SET @num_val = num;
	EXECUTE dynamic_statement USING @num_val;
	DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Delete_Data_Of_WkTables` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Delete_Data_Of_WkTables`()
BEGIN
	DECLARE sqlStr text;
	DECLARE isErr BOOL DEFAULT 0;
    -- EXIT HANDLER エラー メッセージを取得する
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    
	BEGIN
		GET DIAGNOSTICS CONDITION 1 @ERRNO = MYSQL_ERRNO, @MESSAGE_TEXT = MESSAGE_TEXT;
		SELECT 'ERROR' AS STATUS, CONCAT('MySQL ERROR: ', @ERRNO, ': ', @MESSAGE_TEXT) AS MESSAGE;
        ROLLBACK;
	END;
    
    START TRANSACTION;
	SET sqlStr := "DELETE FROM tbl_wk_WordList";
	SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_AFI_Example";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;

    SET sqlStr := "DELETE FROM wkt_Analysis_OBS";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;

    SET sqlStr := "DELETE FROM wkt_Analysis_OBS_Conclusion";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;

    SET sqlStr := "DELETE FROM wkt_Analysis_WC";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_GFA_Examples";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_OBS_Conclusion";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_OBS_Fact";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_PFA_Examples";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;

    SET sqlStr := "DELETE FROM wkt_SOI_Example";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;

    SET sqlStr := "DELETE FROM wkt_STR_Examples";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_TransHistory";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_ErrorData";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_WCList";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_Analysis_AFI_Example";
	SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    
    SET sqlStr := "DELETE FROM wkt_Analysis_GFA_Examples";
    SET @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;

    COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_11Records_Field` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_11Records_Field`(in_PlantName varchar(50), in_DmDivision varchar(1))
BEGIN
     DECLARE strSQL TEXT;
     
	 IF(in_DmDivision = "F") THEN
		SET strSQL :="SELECT * FROM tbl_Field";
        SET strSQL := Concat(strSQL," WHERE (display_order BETWEEN 1 AND 11)");
        SET strSQL := Concat(strSQL," AND dm_div = Fn_Get_Dm_DivisionF(?) "); -- in_PlantName で部品コードを取得します。
        SET strSQL := Concat(strSQL," AND plant_name = ?"); -- in_PlantName
        SET strSQL := Concat(strSQL," ORDER BY display_order LIMIT 11;");
     ELSE
		SET strSQL :="SELECT * FROM tbl_Field";
        SET strSQL := Concat(strSQL," WHERE (display_order BETWEEN 1 AND 11)");
        SET strSQL := Concat(strSQL," AND dm_div = ?"); -- in_DmDivision
        SET strSQL := Concat(strSQL," AND plant_name = ?"); -- in_PlantName
        SET strSQL := Concat(strSQL," ORDER BY display_order LIMIT 11;");
	 END IF;

     SET @sqlexc :=strSQL;
	 PREPARE dynamic_statement FROM @sqlexc;
	 SET @plantName_val= in_PlantName;
	 
     IF(in_DmDivision = "F") THEN
			EXECUTE dynamic_statement USING @plantName_val, @plantName_val;
	 ELSE
			SET @dmDivision_val = in_DmDivision;
			EXECUTE dynamic_statement USING @dmDivision_val, @plantName_val;
	 END IF;
	 DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Field` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Field`(in_PlantName varchar(50))
BEGIN
    DECLARE strSQL TEXT;
    
	SET strSQL :=	"SELECT	tbl_field.display_order as DisplayOrder, tbl_field.fields,  ";-- 分野
	SET strSQL := CONCAT(strSQL, "tbl_field.fields_name AS FieldsName, tbl_field.fields_name_en AS FieldsNameEn "); -- in_Lang
	SET strSQL := CONCAT(strSQL, "FROM	tbl_field " );
    SET strSQL := CONCAT(strSQL, "WHERE	plant_name = ? "); -- in_PlantName
    SET strSQL := CONCAT(strSQL,"ORDER BY DisplayOrder;");-- 表示順
	
    SET @sqlexc :=strSQL;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @plantName_val= in_PlantName;
    EXECUTE dynamic_statement USING @plantName_val;
    DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Initial` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Initial`(in_PlantName varchar(50))
BEGIN
		DECLARE strSQL TEXT;
        
         -- [イニシャル]
         -- 氏名
		SET strSQL := "SELECT tbl_reviewmember.initial,
							  tbl_user.name
					   FROM	tbl_reviewmember
                       INNER JOIN tbl_user ON tbl_reviewmember.initial = tbl_user.initial
					   LEFT JOIN
							tbl_peerreview ON
							tbl_reviewmember.plant_name = tbl_peerreview.plant_name
					   WHERE "; 					-- tbl_ReviewMember.[plant_name][プラント名] = tbl_PeerReview.[plant_name][プラント名]	
		-- (((tbl_PeerReview.[plant_name][プラント名])=G_getPlant()) And
		SET strSQL := CONCAT(strSQL, "(((tbl_peerreview.plant_name)= ? ) And "); -- 1. in_PlantName
        -- ((tbl_reviewmember.tl)=True)) Or 	
		SET strSQL := CONCAT(strSQL, "((tbl_reviewmember.tl)=1)) Or ");
        -- (((tbl_PeerReview.[plant_name][プラント名])=G_getPlant()) And
		SET strSQL := CONCAT(strSQL, "(((tbl_peerreview.plant_name) = ? ) And "); -- 2. in_PlantName
        -- ((tbl_reviewmember.[judge]審査者)=True)) Or
		SET strSQL := CONCAT(strSQL, "((tbl_reviewmember.judge)=1)) Or ");
        -- (((tbl_PeerReview.[plant_name][プラント名])=G_getPlant()) And 	
		SET strSQL := CONCAT(strSQL, "(((tbl_peerreview.plant_name) = ? ) And "); -- 3. in_PlantName
        -- ((tbl_reviewmember.[editor]編集者)=True))
		SET strSQL := CONCAT(strSQL, "((tbl_reviewmember.editor)=1)) ");
		SET strSQL := CONCAT(strSQL, " ORDER BY tbl_reviewmember.initial;");-- [イニシャル]
    
	    SET @sqlexc :=strSQL;
	    PREPARE dynamic_statement FROM @sqlexc;
        SET @plantName_val= in_PlantName;
	    EXECUTE dynamic_statement USING @plantName_val, @plantName_val, @plantName_val;
	    DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Num_List` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Num_List`( in_PlantName varchar(10), in_Num varchar(5000))
BEGIN
	DECLARE strSQL TEXT;
    
	SET strSQL := "SELECT vw_translate_control.offer_num ";
	SET strSQL := CONCAT(strSQL, " FROM vw_obs_extend");
    SET strSQL := CONCAT(strSQL, " INNER JOIN vw_translate_control ");
	SET strSQL := CONCAT(strSQL, "	ON (vw_obs_extend.num = vw_translate_control.num)");
	SET strSQL := CONCAT(strSQL, " WHERE vw_obs_extend.plant_name = ?");
    SET strSQL := CONCAT(strSQL, " AND vw_obs_extend.num = ?");
	SET strSQL := CONCAT(strSQL, " AND (vw_obs_extend.approval_state_pr_rev = '' Or vw_obs_extend.approval_state_pr_rev < vw_obs_extend.past_translated_rev)");
    SET strSQL := CONCAT(strSQL, " AND vw_obs_extend.past_translated_status = True");
	SET strSQL := CONCAT(strSQL, " AND vw_obs_extend.revisions > vw_translate_control.revisions");
	SET strSQL := CONCAT(strSQL, " AND vw_translate_control.trans_situation = '取込済'");
	SET strSQL := CONCAT(strSQL, " ORDER BY vw_translate_control.revisions, vw_translate_control.offer_num;");

    SET @sqlexc :=strSQL;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @plantName_val= in_PlantName;
    SET @num_val = Concat( in_PlantName,'-',in_Num);
    EXECUTE dynamic_statement USING @plantName_val, @num_val;
    DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Obs_ExtEND_List_Clear` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Obs_ExtEND_List_Clear`(in_PlantName varchar(10), in_Lang varchar(1))
BEGIN
    DECLARE strSQL TEXT;
    SET strSQL := "SELECT vw_obs_extEND_filter.* ";
	/*
        -- in_Lang: "ｵﾘｼﾞﾅﾙ版／Original";"0";"日本語版／Japanese";"1";"英語版／English";"2"
	*/  
    IF in_Lang = Fn_Get_Constant('LANG_NUM','EN') THEN
        SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extEND_filter.title_trans = '', 'making', vw_obs_extEND_filter.title_trans) AS l_title"); -- Lタイトル
        SET strSQL := CONCAT(strSQL , ", IF(val_comp = True, 'Complete', 'Incomplete') AS val_status"); -- VAL状況
        SET strSQL := CONCAT(strSQL , ", IF(trans_range = Fn_Get_Constant('TRANS_RANGE','NONE'), 'Nothing', IF(trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL'), 'Partial', IF(trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE'), 'Whole', ''))) AS l_trans_range"); -- L翻訳範囲
        SET strSQL := CONCAT(strSQL , ", IF(approval_state_pr=True,");
        SET strSQL := CONCAT(strSQL , "  CONCAT( '【Reviewer】' , Fn_Jap_Eng_Con_App_Status(Fn_Get_Constant('LANGUAGE','EN'),approval_state_pr_state)),");
        SET strSQL := CONCAT(strSQL , "  CONCAT( IF(approval_state1=True,'【As.1】','') ,  IF(approval_state2=True,'【As.2】','') , IF(approval_state3=True,'【As.3】','') , IF(approval_state_tl=True,'【TL】','') , Fn_Jap_Eng_Con_App_Status(Fn_Get_Constant('LANGUAGE','EN'),approval_state_tl_state) )) AS tl_approval_status");-- TL承認状況
    ELSE
        SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extEND_filter.language = Fn_Get_Constant('LANGUAGE','EN'),");
        SET strSQL := CONCAT(strSQL , " vw_obs_extEND_filter.title_trans,");
        SET strSQL := CONCAT(strSQL , " vw_obs_extEND_filter.title) AS l_title"); -- Lタイトル
        SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extEND_filter.language = Fn_Get_Constant('LANGUAGE','EN'),");
        SET strSQL := CONCAT(strSQL , " IF(val_comp = True, 'Complete', 'Incomplete'),");
        SET strSQL := CONCAT(strSQL , " IF(val_comp = True, '完了', '未完')) AS val_status");-- VAL状況
        SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extEND_filter.language = Fn_Get_Constant('LANGUAGE','EN'),");
        SET strSQL := CONCAT(strSQL , " IF(trans_range = Fn_Get_Constant('TRANS_RANGE','NONE'), 'Nothing', IF(trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL'), 'Partial', IF(trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE'), 'Whole', ''))),");
        SET strSQL := CONCAT(strSQL , " IF(trans_range = Fn_Get_Constant('TRANS_RANGE','NONE'), 'なし', IF(trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL'), '部分', IF(trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE'), '全文', '')))) AS l_trans_range"); -- L翻訳範囲
        SET strSQL := CONCAT(strSQL , ", IF(approval_state_pr=True,");
        SET strSQL := CONCAT(strSQL , "  CONCAT (IF(vw_obs_extEND_filter.language =Fn_Get_Constant('LANGUAGE','EN'),'【Reviewer】','【レビュワ】') , Fn_Jap_Eng_Con_App_Status(vw_obs_extEND_filter.language,approval_state_pr_state)), ");
        SET strSQL := CONCAT(strSQL , "  CONCAT (IF(approval_state1=True,IF(vw_obs_extEND_filter.language =Fn_Get_Constant('LANGUAGE','EN'),'【As.1】','【審1】'),'') , IF(approval_state2=True,IF(vw_obs_extEND_filter.language= Fn_Get_Constant('LANGUAGE','EN'),'【As.2】','【審2】'),'') , ");
        SET strSQL := CONCAT(strSQL , "  IF(approval_state3=True,IF(vw_obs_extEND_filter.language=Fn_Get_Constant('LANGUAGE','EN'),'【As.3】','【審3】'),'') , IF(approval_state_tl=True,'【TL】','') , Fn_Jap_Eng_Con_App_Status(vw_obs_extEND_filter.language,approval_state_tl_state))) AS tl_approval_status");-- TL承認状況
    END IF;
    
    SET strSQL := CONCAT(strSQL , " FROM vw_obs_extEND_filter");
    SET strSQL := CONCAT(strSQL , " WHERE vw_obs_extEND_filter.plant_name = ? ");-- 1.in_PlantName
    SET strSQL := CONCAT(strSQL , " ORDER BY vw_obs_extEND_filter.field_order, vw_obs_extEND_filter.num;"); -- 分野並び, 番号

    SET @sqlexc :=strSQL;
    PREPARE dynamic_statement FROM @sqlexc;
	SET @PlantName_val = in_PlantName;
	EXECUTE dynamic_statement USING @PlantName_val;
    DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Obs_Extend_List_Search` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Obs_Extend_List_Search`(in_PlantName varchar(10), in_Area varchar(30), in_Initial varchar(50), in_Lang varchar(1), in_TL tinyint, in_TxtFreeWord varchar(50))
BEGIN
 -- G_cns区切文字_文字
    DECLARE G_cns_delimiter_character VARCHAR(1000);
    DECLARE strSQL text;
    
    SET G_cns_delimiter_character :="@@@";
	SET strSQL := "SELECT vw_obs_extend_filter.*";
    -- in_Lang: "ｵﾘｼﾞﾅﾙ版／Original";"0";"日本語版／Japanese";"1";"英語版／English";"2"
    CASE in_Lang
		WHEN Fn_Get_Constant('LANG_NUM','JA') THEN
		   SET strSQL := CONCAT(strSQL , ", vw_obs_extend_filter.title AS `l_title`"); -- Lタイトル
		   SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.val_comp= True, '完了', '未完') AS `val_status`"); -- VAL状況
		   SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','NONE'), 'なし', IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL'), '部分', IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE'), '全文', ''))) AS `l_trans_range`"); -- L翻訳範囲
		   SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.approval_state_pr=True,");
		   SET strSQL := CONCAT(strSQL , "  CONCAT( '【レビュワ】' , Fn_Jap_Eng_Con_App_Status('',vw_obs_extend_filter.approval_state_pr_state) ),");
		   SET strSQL := CONCAT(strSQL , "  CONCAT( IF(vw_obs_extend_filter.approval_state1 = True, '【審1】', '') ,  IF(vw_obs_extend_filter.approval_state2 = True, '【審2】', '') , IF(vw_obs_extend_filter.approval_state3 = True, '【審3】', '') , IF(approval_state_tl = True, '【TL】', '') , Fn_Jap_Eng_Con_App_Status('', approval_state_tl_state) )) AS `tl_approval_status`"); -- TL承認状況
		WHEN Fn_Get_Constant('LANG_NUM','EN') THEN
		   SET strSQL := CONCAT(strSQL , ", vw_obs_extend_filter.title_trans AS `l_title`");-- Lタイトル
		   SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.val_comp= True, 'Complete', 'Incomplete') AS `val_status`");-- VAL状況
		   SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','NONE'), 'Nothing', IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL'), 'Partial', IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE'), 'Whole', ''))) AS `l_trans_range`");-- L翻訳範囲
		   SET strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.approval_state_pr=True,");
		   SET strSQL := CONCAT(strSQL , "  CONCAT( '【Reviewer】' , Fn_Jap_Eng_Con_App_Status(Fn_Get_Constant('LANGUAGE','EN'),vw_obs_extend_filter.approval_state_pr_state)),");
		   SET strSQL := CONCAT(strSQL , "  CONCAT( IF(vw_obs_extend_filter.approval_state1 = True, '【As.1】', '') ,  IF(vw_obs_extend_filter.approval_state2 = True, '【As.2】', '') , IF(vw_obs_extend_filter.approval_state3 = True, '【As.3】', '') , IF(approval_state_tl = True, '【TL】','') , Fn_Jap_Eng_Con_App_Status(Fn_Get_Constant('LANGUAGE','EN'), approval_state_tl_state) )) AS `tl_approval_status`");-- TL承認状況
     ELSE
		BEGIN
			  SET  strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.language = Fn_Get_Constant('LANGUAGE','EN'),");
			  SET  strSQL := CONCAT(strSQL , " vw_obs_extend_filter.title_trans" , ",");
			  SET  strSQL := CONCAT(strSQL , " vw_obs_extend_filter.title" , ") AS `l_title`");-- Lタイトル
			  SET  strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.language = Fn_Get_Constant('LANGUAGE','EN'),");
			  SET  strSQL := CONCAT(strSQL , " IF(vw_obs_extend_filter.val_comp= True, 'Complete', 'Incomplete')" , ",");
			  SET  strSQL := CONCAT(strSQL , " IF(vw_obs_extend_filter.val_comp= True, '完了', '未完')" , ") AS `val_status`"); -- VAL状況
			  SET  strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.language = Fn_Get_Constant('LANGUAGE','EN'),");
			  SET  strSQL := CONCAT(strSQL , " IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','NONE'), 'Nothing', IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL'), 'Partial', IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE'), 'Whole', '')))" , ",");
			  SET  strSQL := CONCAT(strSQL , " IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','NONE'), 'なし', IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL'), '部分', IF(vw_obs_extend_filter.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE'), '全文', '')))" , ") AS `l_trans_range`");-- L翻訳範囲
			  SET  strSQL := CONCAT(strSQL , ", IF(vw_obs_extend_filter.approval_state_pr=True,");
			  SET  strSQL := CONCAT(strSQL , "  CONCAT( IF(vw_obs_extend_filter.language = Fn_Get_Constant('LANGUAGE','EN'),'【Reviewer】','【レビュワ】') , Fn_Jap_Eng_Con_App_Status(vw_obs_extend_filter.language, vw_obs_extend_filter.approval_state_pr_state) ),");
			  SET  strSQL := CONCAT(strSQL , "  CONCAT( IF(vw_obs_extend_filter.approval_state1 = True, IF(vw_obs_extend_filter.language = Fn_Get_Constant('LANGUAGE','EN'),'【As.1】','【審1】'),'') , IF(vw_obs_extend_filter.approval_state2=True, IF(vw_obs_extend_filter.language = Fn_Get_Constant('LANGUAGE','EN'),'【As.2】','【審2】'),'') , ");
			  SET  strSQL := CONCAT(strSQL , "   IF(vw_obs_extend_filter.approval_state3 = True, IF(vw_obs_extend_filter.language = Fn_Get_Constant('LANGUAGE','EN'),'【As.3】','【審3】'),'') , IF(vw_obs_extend_filter.approval_state_tl = True,'【TL】',''), Fn_Jap_Eng_Con_App_Status(vw_obs_extend_filter.language, vw_obs_extend_filter.approval_state_tl_state) )) AS `tl_approval_status`");-- TL承認状況
	    END;
    END CASE;
    
    IF in_PlantName <> "" THEN
		SET strSQL := CONCAT(strSQL , " FROM vw_obs_extend_filter WHERE vw_obs_extend_filter.plant_name = ? "); -- 1 :in_PlantName
	ELSE
		SET strSQL := CONCAT(strSQL , " FROM vw_obs_extend_filter ");
	END IF;
    
    IF in_Initial <> "" OR in_Area <> "" OR in_TxtFreeWord <> "" OR in_TL = True THEN
		IF in_PlantName <> "" THEN
			SET strSQL := CONCAT(strSQL , " AND vw_obs_extend_filter.num IN (SELECT Distinct(vw_obs_extend_filter.num)");
        ELSE
			SET strSQL := CONCAT(strSQL , " WHERE vw_obs_extend_filter.num IN (SELECT Distinct(vw_obs_extend_filter.num)");
        END IF;
        
        SET strSQL := CONCAT(strSQL , " FROM vw_obs_extend_filter");
        SET strSQL := CONCAT(strSQL , " LEFT JOIN tbl_obs_conclusion ON vw_obs_extend_filter.num = tbl_obs_conclusion.num");
        SET strSQL := CONCAT(strSQL , " LEFT JOIN tbl_obs_fact ON vw_obs_extend_filter.num = tbl_obs_fact.num");

        IF in_Area <> "" THEN
            SET strSQL := CONCAT(strSQL , " WHERE vw_obs_extend_filter.fields = ? "); -- 2 in_Area
        END IF;
        
        IF in_Initial <> "" THEN
            IF in_Area <> "" THEN
                SET strSQL := CONCAT(strSQL , " AND");
            ELSE
                SET strSQL := CONCAT(strSQL , " WHERE");
            END IF;
            SET strSQL := CONCAT(strSQL , " vw_obs_extend_filter.part_id = ? "); -- 3 in_Initial
        END IF;
        
        IF in_TxtFreeWord <> "" THEN
            IF in_Area <> "" OR in_Initial <> "" THEN
                SET strSQL := CONCAT(strSQL , " AND ");
            Else
                SET strSQL := CONCAT(strSQL , " WHERE ");
            END IF;
            -- in_Lang: "ｵﾘｼﾞﾅﾙ版／Original";"0";"日本語版／Japanese";"1";"英語版／English";"2"
            CASE in_Lang
				WHEN Fn_Get_Constant('LANG_NUM','JA') THEN
					SET strSQL := CONCAT(strSQL , " (vw_obs_extend_filter.title Like ? "); -- 4. in_TxtFreeWord : title
					SET strSQL := CONCAT(strSQL , " OR vw_obs_extend_filter.scope Like  ? "); -- 5. in_TxtFreeWord : scope
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_conclusion.conclusion, '" , G_cns_delimiter_character , "', '') Like ? "); -- 6. in_TxtFreeWord : conclusion
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_fact.fact, '" , G_cns_delimiter_character , "', '') Like  ? " , ")"); -- 7. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 7. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 8. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 9. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 10. in_TxtFreeWord : fact
				WHEN Fn_Get_Constant('LANG_NUM','EN') THEN
					SET strSQL := CONCAT(strSQL , " (vw_obs_extend_filter.title_trans Like  ? "); -- 8. in_TxtFreeWord : title_trans
					SET strSQL := CONCAT(strSQL , " OR vw_obs_extend_filter.scope_trans Like  ? "); -- 9. in_TxtFreeWord : scope_trans
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_conclusion.conclusion_trans, '" , G_cns_delimiter_character , "', '') Like ? "); -- 10. in_TxtFreeWord : conclusion_trans
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_fact.fact_trans, '" , G_cns_delimiter_character , "', '') Like ? " , ")"); -- 11. in_TxtFreeWord : fact_trans
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 7. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 8. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 9. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 10. in_TxtFreeWord : fact
				ELSE
					SET strSQL := CONCAT(strSQL , " IF(vw_obs_extend_filter.language = Fn_Get_Constant('LANGUAGE','EN'),");
					SET strSQL := CONCAT(strSQL , " (vw_obs_extend_filter.title_trans Like ? "); -- 12. in_TxtFreeWord : title_trans
					SET strSQL := CONCAT(strSQL , " OR vw_obs_extend_filter.scope_trans Like ? "); -- 13. in_TxtFreeWord : scope_trans
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_conclusion.conclusion_trans, '" , G_cns_delimiter_character , "', '') Like  ? "); -- 14. in_TxtFreeWord : conclusion_trans
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_fact.fact_trans, '" , G_cns_delimiter_character , "', '') Like ? " , "),"); -- 15. in_TxtFreeWord : fact_trans
					SET strSQL := CONCAT(strSQL , " (vw_obs_extend_filter.title Like ? "); -- 16. in_TxtFreeWord : title
					SET strSQL := CONCAT(strSQL , " OR vw_obs_extend_filter.scope Like ? "); -- 17. in_TxtFreeWord : scope
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_conclusion.conclusion, '" , G_cns_delimiter_character , "', '') Like ? "); -- 18. in_TxtFreeWord : conclusion
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_fact.fact, '" , G_cns_delimiter_character , "', '') Like ? " , "))"); -- 19. in_TxtFreeWord : fact
				END CASE;
        END IF;

        IF in_TL = True THEN
            IF in_Area <> "" OR in_Initial <> "" OR in_TxtFreeWord <> "" THEN
                SET strSQL := CONCAT(strSQL , " AND ");
            Else
                SET strSQL := CONCAT(strSQL , " WHERE ");
            END IF;
            SET strSQL := CONCAT(strSQL , " vw_obs_extend_filter.approval_state_tl = True");
        END IF;

        SET strSQL := CONCAT(strSQL , ")");

        SET strSQL := CONCAT(strSQL , " ORDER BY vw_obs_extend_filter.field_order, vw_obs_extend_filter.num;");
	ELSE
		SET strSQL := CONCAT(strSQL , " ORDER BY vw_obs_extend_filter.field_order, vw_obs_extend_filter.num;");
	END IF;
                 
	SET @sqlexc :=strSQL;
	PREPARE dynamic_statement FROM @sqlexc;
	IF (in_PlantName = "" ) THEN
		BEGIN
			EXECUTE dynamic_statement;
			DEALLOCATE PREPARE dynamic_statement;
		END;
	ELSE
		BEGIN
			IF (in_Initial = "" AND in_Area = "" AND in_TxtFreeWord = "" ) THEN
				BEGIN
                    SET @PlantName_val = in_PlantName;
					EXECUTE dynamic_statement USING @PlantName_val;
					DEALLOCATE PREPARE dynamic_statement;
				END;
			ELSEIF (in_Initial <> "" AND in_Area <> "" AND in_TxtFreeWord <> "" ) THEN
				BEGIN
					SET @PlantName_val = in_PlantName;
					SET @Field_val = in_Area;
					SET @PartId_val = in_Initial;
					SET @txtFreeWord_val = Concat('%', in_TxtFreeWord,'%');
					EXECUTE dynamic_statement USING @PlantName_val, @Field_val, @PartId_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val;
					DEALLOCATE PREPARE dynamic_statement;
				END;
			ELSEIF (in_Initial <> "" AND in_Area <> "" AND in_TxtFreeWord = ""  ) THEN
				BEGIN
					SET @PlantName_val = in_PlantName;
					SET @Field_val = in_Area;
					SET @PartId_val = in_Initial;
					EXECUTE dynamic_statement USING @PlantName_val, @Field_val, @PartId_val;
					DEALLOCATE PREPARE dynamic_statement;
				END;
			ELSEIF (in_Initial <> "" AND in_Area = "" AND in_TxtFreeWord <> ""  ) THEN
				BEGIN
					SET @PlantName_val = in_PlantName;
					SET @PartId_val = in_Initial;
					SET @txtFreeWord_val = Concat('%', in_TxtFreeWord,'%');
					EXECUTE dynamic_statement USING @PlantName_val, @PartId_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val;
					DEALLOCATE PREPARE dynamic_statement;
				END;
			ELSEIF (in_Initial <> "" AND in_Area = "" AND in_TxtFreeWord = ""  ) THEN
				BEGIN
					SET @PlantName_val = in_PlantName;
					SET @PartId_val = in_Initial;
					EXECUTE dynamic_statement USING @PlantName_val, @PartId_val;
					DEALLOCATE PREPARE dynamic_statement;
				END;
			ELSEIF (in_Initial = "" AND in_Area <> "" AND in_TxtFreeWord <> "" ) THEN
				BEGIN
					SET @PlantName_val = in_PlantName;
					SET @Field_val = in_Area;
					SET @txtFreeWord_val = Concat('%', in_TxtFreeWord,'%');
					EXECUTE dynamic_statement USING @PlantName_val, @Field_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val;
					DEALLOCATE PREPARE dynamic_statement;
				END;
			ELSEIF (in_Initial = "" AND in_Area <> "" AND in_TxtFreeWord = "" ) THEN
				BEGIN
					SET @PlantName_val = in_PlantName;
					SET @Field_val = in_Area;
					EXECUTE dynamic_statement USING @PlantName_val, @Field_val;
					DEALLOCATE PREPARE dynamic_statement;
				END;
			ELSEIF (in_Initial = "" AND in_Area = "" AND in_TxtFreeWord <> "" ) THEN
				BEGIN
					SET @PlantName_val = in_PlantName;
					SET @txtFreeWord_val = Concat('%', in_TxtFreeWord,'%');
					EXECUTE dynamic_statement USING @PlantName_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val,
													@txtFreeWord_val, @txtFreeWord_val;
					DEALLOCATE PREPARE dynamic_statement;
				END;
            END IF;
		END;
	END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Obs_Extend_Refer_Obs_Conclusion` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Obs_Extend_Refer_Obs_Conclusion`(in_PlantName  varchar(10)
																			  ,in_Kinds  varchar(3)
																			  ,in_Fields  varchar(4)
																			  ,in_PartId  varchar(3)
																			  ,in_SerialNum  int
																			  ,in_Revisions  int)
BEGIN
	DECLARE strSQL TEXT;
	SET strSQL :="SELECT 
						vw_obs_extend_refer.revisions ,
                        tbl_obs_conclusion.quantity ,
                        tbl_obs_conclusion.seq_num ,
                        tbl_obs_conclusion.conclusion ,
                        tbl_obs_conclusion.conclusion_trans
					FROM vw_obs_extend_refer 
						INNER JOIN tbl_obs_conclusion 
						ON vw_obs_extend_refer.num = tbl_obs_conclusion.num
						WHERE vw_obs_extend_refer.plant_name = ?
						AND vw_obs_extend_refer.kinds = ?
						AND vw_obs_extend_refer.fields = ?
						AND vw_obs_extend_refer.part_id = ?
						AND vw_obs_extend_refer.serial_num = ?
						AND vw_obs_extend_refer.revisions <?
						AND vw_obs_extend_refer.translated_state = true
						ORDER BY vw_obs_extend_refer.revisions DESC;";
                        
       SET @sqlexc :=strSQL;
	   PREPARE dynamic_statement FROM @sqlexc;
       SET @plantName_val = in_PlantName;
       SET @kinds_val = in_Kinds;
       SET @fields_val = in_Fields;
       SET @partId_val = in_PartId;
       SET @serialNum_val = in_SerialNum;
       SET @revisions_val = in_Revisions;
	   EXECUTE dynamic_statement USING @plantName_val, @kinds_val, @fields_val,
                                       @partId_val, @serialNum_val, @revisions_val;
	   DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Obs_Extend_Refer_Obs_Fact` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Obs_Extend_Refer_Obs_Fact`(   in_PlantName  varchar(10)
																		  ,in_Kinds  varchar(3)
																		  ,in_Fields  varchar(4)
																		  ,in_PartId  varchar(3)
																		  ,in_SerialNum  int
																		  ,in_Revisions  int)
BEGIN
	DECLARE strSQL TEXT;
	SET strSQL :="SELECT 
						vw_obs_extend_refer.revisions ,
                        tbl_obs_fact.quantity ,
                        tbl_obs_fact.seq_num ,
                        tbl_obs_fact.fact ,
                        tbl_obs_fact.fact_trans
					FROM vw_obs_extend_refer					 
						INNER JOIN tbl_obs_fact 
						ON vw_obs_extend_refer.num = tbl_obs_fact.num
						WHERE vw_obs_extend_refer.plant_name = ?
						AND vw_obs_extend_refer.kinds = ?
						AND vw_obs_extend_refer.fields = ?
						AND vw_obs_extend_refer.part_id = ?
						AND vw_obs_extend_refer.serial_num = ?
						AND vw_obs_extend_refer.revisions <?
						AND vw_obs_extend_refer.translated_state = true
						ORDER BY vw_obs_extend_refer.revisions DESC;";
                        
       SET @sqlexc :=strSQL;
	   PREPARE dynamic_statement FROM @sqlexc;
       SET @plantName_val = in_PlantName;
       SET @kinds_val = in_Kinds;
       SET @fields_val = in_Fields;
       SET @partId_val = in_PartId;
       SET @serialNum_val = in_SerialNum;
       SET @revisions_val = in_Revisions;
	   EXECUTE dynamic_statement USING @plantName_val, @kinds_val, @fields_val,
                                       @partId_val, @serialNum_val, @revisions_val;
	   DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Obs_List_For_Req_Tran2` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Obs_List_For_Req_Tran2`(
in_PlantName varchar(10)
, in_Kinds varchar(3)
, in_Field varchar(30)
, in_PartId varchar(3)
, in_SerialNum int
, in_Revisions int
, in_Lang varchar(1)
)
BEGIN
	DECLARE strSQL TEXT;
    
	SET strSQL := "SELECT id, plant_name, kinds, fields, part_id, serial_num, revisions, english_edition, title,";
	SET strSQL := Concat(strSQL," title_trans, editor, approval_state_pr_rev, trans_range, trans_deadline, trans_lang, last_update");
	SET strSQL := Concat(strSQL," FROM tbl_obs WHERE trans_state_req = true");
	SET strSQL := Concat(strSQL," AND	translated_state = false ");
	SET strSQL := Concat(strSQL," AND plant_name = ?"); -- 1 {obsDto.PlantName}
	SET strSQL := Concat(strSQL," AND hold <> Fn_Get_Constant('HOLD_FLAG','Active') AND delete_flag = Fn_Get_Constant('DELETE_FLAG','ON')  AND trans_range IN (Fn_Get_Constant('TRANS_RANGE','PARTIAL') , Fn_Get_Constant('TRANS_RANGE','WHOLE')) ");
	SET strSQL := Concat(strSQL," AND kinds = ? "); -- 2{obsDto.Kinds}
	SET strSQL := Concat(strSQL," AND fields = ? "); -- 3{obsDto.Fields}
	SET strSQL := Concat(strSQL," AND part_id = ? "); -- 4{obsDto.PartId}
	SET strSQL := Concat(strSQL," AND serial_num = ? "); -- 5{obsDto.SerialNum}
	SET strSQL := Concat(strSQL," AND revisions = ? ");  -- 6{obsDto.Revisions}
	SET strSQL := Concat(strSQL," AND english_edition = ?"); -- 7{obsDto.EnglishEdition}
	SET strSQL := Concat(strSQL," AND NOT EXISTS (SELECT * FROM vw_translate_control ");
	SET strSQL := Concat(strSQL," WHERE plant_name = tbl_obs.plant_name");
	SET strSQL := Concat(strSQL," AND kinds = tbl_obs.kinds");
	SET strSQL := Concat(strSQL," AND fields = tbl_obs.fields");
	SET strSQL := Concat(strSQL," AND part_id = tbl_obs.part_id");
	SET strSQL := Concat(strSQL," AND serial_num = tbl_obs.serial_num");
	SET strSQL := Concat(strSQL," AND revisions = tbl_obs.revisions");
	SET strSQL := Concat(strSQL," AND cansel_status in ( '' , '取り消し依頼中' , '翻訳継続')");
	SET strSQL := Concat(strSQL," AND plant_name = ?"); -- 8{obsDto.PlantName}
	SET strSQL := Concat(strSQL," AND kinds = ?"); -- 9{obsDto.Kinds}
	SET strSQL := Concat(strSQL," AND fields = ?"); -- 10{obsDto.Fields}
	SET strSQL := Concat(strSQL," AND part_id = ?") ; -- 11{obsDto.PartId}
	SET strSQL := Concat(strSQL," AND serial_num = ?");-- 12{obsDto.SerialNum}
	SET strSQL := Concat(strSQL," AND revisions = ?"); -- 13{obsDto.Revisions}
	SET strSQL := Concat(strSQL," AND english_edition = ?"); -- 14{obsDto.EnglishEdition}
	SET strSQL := Concat(strSQL,");");

    SET @sqlexc :=strSQL;
    PREPARE dynamic_statement FROM @sqlexc;
	SET @PlantName_val = in_PlantName;
    SET @Kinds_val = in_Kinds;
    SET @Field_val = in_Field;
    SET @PartId_val = in_PartId;
    SET @SerialNum_val = in_SerialNum;
    SET @Revisions_val = in_Revisions;
    SET @Lang_val = in_Lang;
	EXECUTE dynamic_statement USING @PlantName_val,@Kinds_val,@Field_val,@PartId_val,@SerialNum_val,@Revisions_val,@Lang_val,
									@PlantName_val,@Kinds_val,@Field_val,@PartId_val,@SerialNum_val,@Revisions_val,@Lang_val;
    DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Peer_Review_Master` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Peer_Review_Master`(in_IsExistsField bool, in_Plant_Name varchar(10), in_Initial varchar(50))
BEGIN
	DECLARE strSQl TEXT;
    
    IF (length(trim(in_Plant_Name)) > 0) THEN
		BEGIN
			SET strSQl := "SELECT tbl_user.respns_area as RespnsArea, 
								  tbl_user.language as Language,
								  tbl_user.coordinator as Coordinator,
								  tbl_reviewmember.TL,
								  tbl_reviewmember.judge as Judge ,
								  tbl_reviewmember.trans as Trans
      						FROM tbl_peerreview
							INNER JOIN tbl_reviewmember ";
            SET strSQl := concat(strSQl," ON tbl_peerreview.plant_name = tbl_reviewmember.plant_name ");            
			SET strSQl := concat(strSQl," INNER JOIN tbl_user ON tbl_reviewmember.initial = tbl_user.initial ");
			SET strSQl := concat(strSQl," WHERE tbl_peerreview.plant_name = ? "); -- 1. plant_Name
			SET strSQl := concat(strSQl,"   AND tbl_reviewmember.initial = ? ;"); -- 2.cmb_Initial
		END;
    ELSE
		SET strSQl := "SELECT respns_area as RespnsArea,
							  language as Language,
                              coordinator as Coordinator,
                              0 as TL,
                              0 as Judge,
                              trans as Trans
                              FROM tbl_user";
                              
		SET strSQl := concat(strSQl," WHERE tbl_user.initial = ? "); -- 3:イニシャル: initial
        SET strSQl := concat(strSQl,"   AND tbl_user.valid = true ;"); -- valid: 有効
    END IF;

   SET @sqlexc :=strSQl;

   PREPARE dynamic_statement FROM @sqlexc;
   SET @plant_Name_val = in_Plant_Name;
   SET @initial_val = in_Initial;

   IF (length(trim(in_Plant_Name)) > 0) THEN
		EXECUTE dynamic_statement USING @plant_Name_val, @initial_val;
   ELSE
		EXECUTE dynamic_statement USING @initial_val;
   END IF;

   DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_WktTransHistoryByParentAndChildTableName` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_WktTransHistoryByParentAndChildTableName`(
	pParentTableName varchar(500)
	,pChildTableName varchar(500)
	,pItemName  varchar(500)
	,pItemNameJp  varchar(500)
	,pItemNameEn  varchar(500)
	,pPlantName varchar(10)
	, pKinds varchar(30)
	, pField varchar(50)
	, pPartId varchar(50)
	, pSerialNum int
	, pRevisions int
	, pLanguage varchar(1)
)
BEGIN
	DECLARE strSQL TEXT;
    
	SET strSQL ='';
	SET strSQL := CONCAT(strSQL ,"SELECT ");
	SET strSQL := CONCAT(strSQL , "tbl_number_control.revisions ");
    SET strSQL := CONCAT(strSQL ,", tbl_number_control.serial_num");
    SET strSQL := CONCAT(strSQL ,",", pChildTableName,".",pItemNameJp," AS item_name_jp ");
    SET strSQL := CONCAT(strSQL ,",", pChildTableName,".",pItemNameEn," AS item_name_en ");
	SET strSQL := CONCAT(strSQL ,",", pChildTableName,".part_trans");
    SET strSQL := CONCAT(strSQL ,",", pParentTableName,".translated_state ");
    SET strSQL := CONCAT(strSQL ,",", pParentTableName,".trans_range ");
    SET strSQL := CONCAT(strSQL ,",", pParentTableName,".num ");
	SET strSQL := CONCAT(strSQL ," FROM  ", pParentTableName);
    SET strSQL := CONCAT(strSQL ," INNER JOIN  tbl_number_control ");
    SET strSQL := CONCAT(strSQL ," ON  ", pParentTableName,".num = tbl_number_control.num ");
    SET strSQL := CONCAT(strSQL ," INNER JOIN  ",pChildTableName);
    SET strSQL := CONCAT(strSQL ," ON  ", pParentTableName,".num = ", pChildTableName,".num");
	SET strSQL := CONCAT(strSQL ," WHERE ", pParentTableName,".plant_name =  '",pPlantName,"'"); 
	SET strSQL := CONCAT(strSQL ," AND ", pParentTableName,".kinds =  '", pKinds,"'"); 
	SET strSQL := CONCAT(strSQL ," AND ", pParentTableName,".fields =  '",pField,"'");
	SET strSQL := CONCAT(strSQL ," AND ", pParentTableName,".part_id =  '",pPartId,"'"); 
	SET strSQL := CONCAT(strSQL ," AND ", pParentTableName,".serial_num =  '",pSerialNum,"'"); 
	SET strSQL := CONCAT(strSQL ," AND ", pParentTableName,".revisions <=  ",pRevisions);
    
    IF (pLanguage is null) THEN
		SET strSQL := CONCAT(strSQL ," AND ", pParentTableName,".language is null "); 
    ELSE
		SET strSQL := CONCAT(strSQL ," AND ", pParentTableName,".language =  '",pLanguage,"'"); 
    END IF;
    
	SET strSQL := CONCAT(strSQL ," AND ", pParentTableName,".translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') ");
	SET strSQL := CONCAT(strSQL ," AND (", pParentTableName,".trans_range =  Fn_Get_Constant('TRANS_RANGE','PARTIAL') OR ", pParentTableName,".trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE')) ");
	SET strSQL := CONCAT(strSQL ," ORDER BY ", pParentTableName,".revisions ;"); 

   SET @sqlexc :=strSQl;
   PREPARE dynamic_statement FROM @sqlexc;
   EXECUTE dynamic_statement; 
   DEALLOCATE PREPARE dynamic_statement;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_InsertWktTransHistoryByObsExtRefAndChildTableName` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_InsertWktTransHistoryByObsExtRefAndChildTableName`(
									 pDisplayOrder int
									,pItemName  varchar(500)
									,pItemNameJp  varchar(500)
									,pItemNameEn  varchar(500)
									,pPlantName varchar(10)
									, pKinds varchar(30)
									, pField varchar(50)
                                    , pPartId varchar(50)
									, pSerialNum int
									, pRevisions int
									, pEnglishEdition varchar(1))
BEGIN
	DECLARE strSQL TEXT;
	-- EXIT HANDLER エラー メッセージを取得する
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    
	BEGIN
		GET DIAGNOSTICS CONDITION 1 @ERRNO = MYSQL_ERRNO, @MESSAGE_TEXT = MESSAGE_TEXT;
		SELECT 'ERROR' AS STATUS, CONCAT('MySQL ERROR: ', @ERRNO, ': ', @MESSAGE_TEXT) AS MESSAGE;
        ROLLBACK;
	END;
    
    START TRANSACTION;
	SET strSQL ='';
    SET strSQL := "INSERT INTO wkt_TransHistory ";
	SET strSQL := CONCAT(strSQL ,"(");
	SET strSQL := CONCAT(strSQL , " revisions ");
	SET strSQL := CONCAT(strSQL ,", item ");
	SET strSQL := CONCAT(strSQL ,", japanese ");
	SET strSQL := CONCAT(strSQL ,", english ");
	SET strSQL := CONCAT(strSQL ,", translated ");
	SET strSQL := CONCAT(strSQL ,", display_order ");
	SET strSQL := CONCAT(strSQL ,")");
	SET strSQL := CONCAT(strSQL ,"SELECT ");
	SET strSQL := CONCAT(strSQL ,"vw_obs_extend_refer.revisions ");
	SET strSQL := CONCAT(strSQL ,",  '",pItemName,  "' AS Item");
	SET strSQL := CONCAT(strSQL ,", If(vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And (vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') OR vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') )) ");
	SET strSQL := CONCAT(strSQL ,",  ","vw_obs_extend_refer.", pItemNameJp);
	SET strSQL := CONCAT(strSQL ,", '') AS japanese ");
	SET strSQL := CONCAT(strSQL ,", If(vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And (vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') OR vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') ) ");
	SET strSQL := CONCAT(strSQL ,", ","vw_obs_extend_refer.",pItemNameEn); 
	SET strSQL := CONCAT(strSQL ,", '') AS english ");
	SET strSQL := CONCAT(strSQL ,", ","If(vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') ");
	SET strSQL := CONCAT(strSQL ,", ","'全文／Whole'");
	SET strSQL := CONCAT(strSQL ,", ","If(vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL')  ");
	SET strSQL := CONCAT(strSQL ,", '部分／Partial' ");
	SET strSQL := CONCAT(strSQL ,", '')) AS translated ");
	SET strSQL := CONCAT(strSQL ,",  ",pDisplayOrder); 
	SET strSQL := CONCAT(strSQL ," FROM vw_obs_extend_refer ");
	SET strSQL := CONCAT(strSQL ," WHERE vw_obs_extend_refer.plant_name =  '",pPlantName,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.kinds =  '", pKinds,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.fields =  '",pField,"'");
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.part_id =  '",pPartId,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.serial_num =  '",pSerialNum,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.revisions <=  ",pRevisions);
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.english_edition =  '",pEnglishEdition,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON')  ");
	SET strSQL := CONCAT(strSQL ," AND (vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') OR vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE')) ");
	SET strSQL := CONCAT(strSQL ," ORDER BY vw_obs_extend_refer.revisions ;"); 

    SET @sqlexc :=strSQl;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement; 
    DEALLOCATE PREPARE dynamic_statement;
    COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_InsertWktTransHistoryByObsExtRefAndScope` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_InsertWktTransHistoryByObsExtRefAndScope`(
									 pDisplayOrder int
									,pItemName  varchar(500)
									,pItemNameJp  varchar(500)
									,pItemNameEn  varchar(500)
									,pPlantName varchar(10)
									, pKinds varchar(30)
									, pField varchar(50)
                                    , pPartId varchar(50)
									, pSerialNum int
									, pRevisions int
									, pLanguage varchar(1))
BEGIN
	DECLARE strSQL TEXT;
    -- EXIT HANDLER エラー メッセージを取得する
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    
	BEGIN
		GET DIAGNOSTICS CONDITION 1 @ERRNO = MYSQL_ERRNO, @MESSAGE_TEXT = MESSAGE_TEXT;
		SELECT 'ERROR' AS STATUS, CONCAT('MySQL ERROR: ', @ERRNO, ': ', @MESSAGE_TEXT) AS MESSAGE;
        ROLLBACK;
	END;
    
    START TRANSACTION;
	SET strSQL ='';
    SET strSQL := "INSERT INTO wkt_TransHistory ";
	SET strSQL := CONCAT(strSQL ,"(");
	SET strSQL := CONCAT(strSQL , " revisions ");
	SET strSQL := CONCAT(strSQL ,", item ");
	SET strSQL := CONCAT(strSQL ,", japanese ");
	SET strSQL := CONCAT(strSQL ,", english ");
	SET strSQL := CONCAT(strSQL ,", translated ");
	SET strSQL := CONCAT(strSQL ,", display_order ");
	SET strSQL := CONCAT(strSQL ,")");
	SET strSQL := CONCAT(strSQL ,"SELECT ");
	SET strSQL := CONCAT(strSQL ,"vw_obs_extend_refer.revisions ");
	SET strSQL := CONCAT(strSQL ,",  '",pItemName,  "' AS Item");
	SET strSQL := CONCAT(strSQL ,", If(vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And (vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') OR vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') ) ");
	SET strSQL := CONCAT(strSQL ,",  ","vw_obs_extend_refer.", pItemNameJp);
	SET strSQL := CONCAT(strSQL ,", '') AS japanese ");
	SET strSQL := CONCAT(strSQL ,", If(vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And (vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') OR vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') ) ");
	SET strSQL := CONCAT(strSQL ,", ","vw_obs_extend_refer.",pItemNameEn); 
	SET strSQL := CONCAT(strSQL ,", '') AS english ");
	SET strSQL := CONCAT(strSQL ,", ","If(vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') ");
	SET strSQL := CONCAT(strSQL ,", ","'全文／Whole'");
	SET strSQL := CONCAT(strSQL ,", ","If(vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL')  ");
	SET strSQL := CONCAT(strSQL ,", '部分／Partial' ");
	SET strSQL := CONCAT(strSQL ,", '')) AS translated ");
	SET strSQL := CONCAT(strSQL ,",  ",pDisplayOrder); 
	SET strSQL := CONCAT(strSQL ," FROM vw_obs_extend_refer ");
	SET strSQL := CONCAT(strSQL ," WHERE vw_obs_extend_refer.plant_name =  '",pPlantName,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.kinds =  '", pKinds,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.fields =  '",pField,"'");
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.part_id =  '",pPartId,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.serial_num =  '",pSerialNum,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.revisions <=  ",pRevisions);
    
    IF(pLanguage IS NULL) THEN
		SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.language is null  "); 
	ELSE
		SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.language =  '",pLanguage,"'"); 
    END IF;
    
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON')  ");
	SET strSQL := CONCAT(strSQL ," AND (vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') OR vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE')) ");
	SET strSQL := CONCAT(strSQL ," ORDER BY vw_obs_extend_refer.revisions ;"); 
    
    SET @sqlexc :=strSQl;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_InsertWktTransHistoryByParentTableName` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_InsertWktTransHistoryByParentTableName`(pParentTableName varchar(500)
									,pDisplayOrder int
									,pItemName  varchar(500)
									,pItemNameJp  varchar(500)
									,pItemNameEn  varchar(500)
									,pPlantName varchar(10)
									, pKinds varchar(30)
									, pField varchar(50)
                                    , pPartId varchar(50)
									, pSerialNum int
									, pRevisions int
									, pEnglishEdition varchar(1))
BEGIN
	DECLARE strSQL TEXT;
    -- EXIT HANDLER エラー メッセージを取得する
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    
	BEGIN
		GET DIAGNOSTICS CONDITION 1 @ERRNO = MYSQL_ERRNO, @MESSAGE_TEXT = MESSAGE_TEXT;
		SELECT 'ERROR' AS STATUS, CONCAT('MySQL ERROR: ', @ERRNO, ': ', @MESSAGE_TEXT) AS MESSAGE;
        ROLLBACK;
	END;
    
    START TRANSACTION;
	SET strSQL ='';
    SET strSQL := "INSERT INTO wkt_TransHistory ";
	SET strSQL := CONCAT(strSQL ,"(");
	SET strSQL := CONCAT(strSQL , " revisions ");
	SET strSQL := CONCAT(strSQL ,", item ");
	SET strSQL := CONCAT(strSQL ,", japanese ");
	SET strSQL := CONCAT(strSQL ,", english ");
	SET strSQL := CONCAT(strSQL ,", translated ");
	SET strSQL := CONCAT(strSQL ,", display_order ");
	SET strSQL := CONCAT(strSQL ,")");
	SET strSQL := CONCAT(strSQL ,"SELECT ");
	SET strSQL := CONCAT(strSQL ,"revisions ");
	SET strSQL := CONCAT(strSQL ,",  '",pItemName,  "' AS Item");
    
    IF(pParentTableName <> 'tbl_WC_past') THEN
		SET strSQL := CONCAT(strSQL ,", If(translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And (trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') OR (trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') AND ");
		SET strSQL := CONCAT(strSQL ," part_trans= Fn_Get_Constant('PART_TRANS','ON') ))  "); 
		SET strSQL := CONCAT(strSQL ,",  ", pItemNameJp);
		SET strSQL := CONCAT(strSQL ,", '') AS japanese ");
		SET strSQL := CONCAT(strSQL ,", If(translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON') And (trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') OR (trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') AND ");
		SET strSQL := CONCAT(strSQL ," part_trans= Fn_Get_Constant('PART_TRANS','ON') ))  ");
		SET strSQL := CONCAT(strSQL ,", ",pItemNameEn); 
		SET strSQL := CONCAT(strSQL ,", '') AS english ");
		SET strSQL := CONCAT(strSQL ,", ","If(translated_state = Fn_Get_Constant('TRANSLATED_STATE'),'ON' And trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE') ");
		SET strSQL := CONCAT(strSQL ,", ","'全文／Whole'");
		SET strSQL := CONCAT(strSQL ,", ","If(translated_state = Fn_Get_Constant('TRANSLATED_STATE'),'ON' And trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') AND ");
		SET strSQL := CONCAT(strSQL ," part_trans = Fn_Get_Constant('PART_TRANS','ON')  ");
		SET strSQL := CONCAT(strSQL ,", '部分／Partial' ");
		SET strSQL := CONCAT(strSQL ,", '')) AS translated ");
    ELSE
		SET strSQL := CONCAT(strSQL ,",  IF(translated_state = Fn_Get_Constant('TRANSLATED_STATE'),'ON' AND trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE')");
        SET strSQL := CONCAT(strSQL ,",  ",pItemNameJp);
        SET strSQL := CONCAT(strSQL ,", '') AS japanese ");
        SET strSQL := CONCAT(strSQL ,",  IF(translated_state = Fn_Get_Constant('TRANSLATED_STATE'),'ON' AND trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE')");
        SET strSQL := CONCAT(strSQL ,",  ",pItemNameEn);
        SET strSQL := CONCAT(strSQL ,", '') AS english ");
		SET strSQL := CONCAT(strSQL ,",  IF(translated_state = Fn_Get_Constant('TRANSLATED_STATE'),'ON' AND trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE')");
		SET strSQL := CONCAT(strSQL ,", ","'全文／Whole'");
		SET strSQL := CONCAT(strSQL ,", '') AS Translated ");
    END IF;

	SET strSQL := CONCAT(strSQL ,",  ",pDisplayOrder); 
	SET strSQL := CONCAT(strSQL ," FROM  ",pParentTableName);
	SET strSQL := CONCAT(strSQL ," WHERE vw_obs_extend_refer.plant_name =  '",pPlantName,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.kinds =  '", pKinds,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.fields =  '",pField,"'");
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.part_id =  '",pPartId,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.serial_num =  '",pSerialNum,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.revisions <=  ",pRevisions);
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.english_edition =  '",pEnglishEdition,"'"); 
	SET strSQL := CONCAT(strSQL ," AND vw_obs_extend_refer.translated_state = Fn_Get_Constant('TRANSLATED_STATE','ON'  ");
	SET strSQL := CONCAT(strSQL ," AND (vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','PARTIAL') OR vw_obs_extend_refer.trans_range = Fn_Get_Constant('TRANS_RANGE','WHOLE')) ");
	SET strSQL := CONCAT(strSQL ," ORDER BY vw_obs_extend_refer.revisions ;"); 

	SET @sqlexc :=strSQl;
    PREPARE dynamic_statement FROM @sqlexc;
    EXECUTE dynamic_statement;
    DEALLOCATE PREPARE dynamic_statement;
    COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Set_Data_List_To_Print` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Set_Data_List_To_Print`(
	in_SheetName varchar(1000), in_CallForm varchar(1000),
	in_PlantName varchar(10), in_Area varchar(30),
	in_Initial varchar(50), in_Lang varchar(1),
	in_TL tinyint, in_TxtFreeWord varchar(50))
BEGIN
	DECLARE strSQL text;
	DECLARE strTableName text;
	
	SET strSQL := "";
    SET strTableName := "";
    
	CASE in_SheetName
		WHEN "GFA" THEN
			SET strTableName := "tbl_gfa_extend";
		WHEN "PFA" THEN
			SET strTableName := "tbl_pfa_extend";
		WHEN "AFI" THEN
			SET strTableName := "tbl_afi_extend";
		WHEN "BP" THEN
			SET strTableName := "tbl_bp_extend";
		WHEN "OBS" THEN
			SET strTableName := "vw_obs_extend";
		WHEN "PD" THEN
			SET strTableName := "tbl_pd_extend";
		WHEN "STR" THEN
			SET strTableName := "tbl_str_extend";
		WHEN "SOI" THEN
			SET strTableName := "tbl_soi_extend";
		ELSE
			BEGIN
            END;
	END CASE;
		   
	SET strSQL := "DELETE FROM tbl_wk_WordList;";
	
	SET @sqlexc :=strSQL;
	PREPARE dynamic_statement FROM @sqlexc;
	EXECUTE dynamic_statement;
	DEALLOCATE PREPARE dynamic_statement;

	IF in_CallForm = "frm_ADMenu" THEN
		SET strSQL := CONCAT("SELECT " , strTableName , ".id");
		SET strSQL := CONCAT(strSQL, ", " , strTableName , ".new_num");
		SET strSQL := CONCAT(strSQL, ", " , strTableName , ".fields");
        
		-- in_Lang: "ｵﾘｼﾞﾅﾙ版／Original";"0";"日本語版／Japanese";"1";"英語版／English";"2"
		CASE in_Lang
			WHEN Fn_Get_Constant('LANG_NUM','JA') THEN
				SET strSQL := CONCAT(strSQL, ", " , strTableName , ".title");
			WHEN Fn_Get_Constant('LANG_NUM','EN') THEN
				SET strSQL := CONCAT(strSQL, ", " , strTableName , ".title_trans");
			ELSE
				SET strSQL := CONCAT(strSQL, ", If(", strTableName , ".language = Fn_Get_Constant('LANGUAGE','EN'), " , strTableName , ".title_trans, null ) AS title_trans ");
                SET strSQL := CONCAT(strSQL, ", If(", strTableName , ".language <> Fn_Get_Constant('LANGUAGE','EN'), " , strTableName , ".title, null ) AS title");
		END CASE;

		IF in_SheetName <> "GFA" AND in_SheetName <> "PFA" Then
			SET strSQL := CONCAT(strSQL,  ", " , strTableName , ".package_exclude");
		ELSE
			SET strSQL := CONCAT(strSQL,", null as package_exclude");
		END IF;
		SET strSQL := CONCAT(strSQL,", null as plant_name,null as years,
									   null as kinds,null as part_id,
									   null as serial_num,null as revisions,
									   null as part_trans,null as observation_target,
									   null as final_ver,null as val_comp,null as general_jdc,
									   null as trans_range,null as trans_deadline,null as trans_lang,null as trans_state_req,
									   null as trans_state_req_id,null as trans_req_date,null as translating_state,null as translating_state_id,
									   null as translated_state,null as translated_state_id,null as approval_state_pr,null as approval_state_pr_id,
									   null as approval_state_pr_rev,null as approval_state_pr_state,null as approval_state1,null as approval_state1_id,
									   null as approval_state1_rev,null as approval_state2,null as approval_state2_id,null as approval_state2_rev,
									   null as approval_state3,null as approval_state3_id,null as approval_state3_rev,null as approval_state_tl,
									   null as approval_state_tl_id,null as approval_state_tl_rev,null as approval_state_tl_state,null as language,
									   null as hold,null as newest_flag,null as delete_flag,null as editor,
									   null as last_update,null as last_user,null as l_num,
									   null as field_order,null as trans_situation,null as trans_cansel,null as past_translated_rev,
									   null as past_translated_status,null as l_title,null as val_status,null as l_trans_range,
									   null as tl_approval_status");
		SET strSQL := CONCAT(strSQL,  " FROM " , strTableName);
		SET strSQL := CONCAT(strSQL,  " WHERE (" , strTableName , ".newest_flag <> Fn_Get_Constant('NEWEST_FLAG','OFF'))");
        SET strSQL := CONCAT(strSQL,  " AND (" , strTableName , ".plant_name = ?)");
		SET strSQL := CONCAT(strSQL, " AND (" , strTableName , ".delete_flag = Fn_Get_Constant('DELETE_FLAG','OFF'))");

		IF in_SheetName <> "GFA" AND in_SheetName <> "PFA" THEN
			SET strSQL := CONCAT(strSQL, " AND (" , strTableName , ".package_exclude = 0)");
		END IF;

		SET strSQL := CONCAT( strSQL, " ORDER BY " , strTableName , ".FieldOrder, " , strTableName , ".Num");
		-- select strSQL;
		SET @sqlexc :=strSQL;
		PREPARE dynamic_statement FROM @sqlexc;
        SET @plantName_val= in_PlantName;
		EXECUTE dynamic_statement USING @plantName_val;
		DEALLOCATE PREPARE dynamic_statement;
        
	ELSEIF in_CallForm = "frm_PastData_Search" THEN
		Select 'To do';
	ELSE
	    CASE in_SheetName
			WHEN "OBS" THEN
				CALL Sp_Get_Obs_Extend_List_Search (in_PlantName, in_Area, in_Initial, in_Lang, in_TL, in_TxtFreeWord);
			ELSE
				BEGIN
                END;
		END CASE;
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SP_Sort_wk_WordList` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `SP_Sort_wk_WordList`()
BEGIN
    SELECT * 
    FROM tbl_wk_WordList
    -- ORDER BY If(Not IsNull(変更後順序), 変更後順序, 印刷順序 + 100000);
    ORDER BY If(Not IsNull(after_change_order), after_change_order, print_order + 100000);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Review_Member_Editor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Review_Member_Editor`(in_plantName varchar(50))
BEGIN
	SELECT DISTINCT
   initial,
   name,
   tl_judge,
   comment,
   respns_area
FROM
 ( select * from 
 (
	SELECT  tbl_reviewmember.initial,
			tbl_user.name,
			tbl_reviewmember.tl_judge,
			tbl_reviewmember.comment,
			tbl_user.respns_area,
			tbl_field.display_order
	FROM tbl_reviewmember 
	INNER JOIN tbl_user 
			ON tbl_reviewmember.initial = tbl_user.initial
	INNER JOIN tbl_peerreview 
	ON tbl_reviewmember.plant_name = tbl_peerreview.plant_name
	LEFT JOIN tbl_field 
	ON (tbl_user.respns_area = tbl_field.fields) 
	AND (tbl_reviewmember.plant_name = tbl_field.plant_name)
	WHERE tbl_peerreview.plant_name = in_plantName
	AND tbl_reviewmember.tl = true
	union ALL
	SELECT  tbl_reviewmember.initial,
			tbl_user.name,
			tbl_reviewmember.tl_judge,
			tbl_reviewmember.comment,
			tbl_user.respns_area,
			tbl_field.display_order
	FROM tbl_reviewmember 
	INNER JOIN tbl_user 
			ON tbl_reviewmember.initial = tbl_user.initial
	INNER JOIN tbl_peerreview 
	ON tbl_reviewmember.plant_name = tbl_peerreview.plant_name
	LEFT JOIN tbl_field 
	ON (tbl_user.respns_area = tbl_field.fields) 
	AND (tbl_reviewmember.plant_name = tbl_field.plant_name)
	WHERE tbl_peerreview.plant_name = in_plantName
	AND tbl_reviewmember.judge = true
   ) AS tblTemp
ORDER BY tblTemp.display_order, tblTemp.respns_area, tblTemp.initial) AS tblTemp2;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `vw_obs_control`
--

/*!50001 DROP VIEW IF EXISTS `vw_obs_control`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_obs_control` AS select `tbl_obs`.`num` AS `num`,`tbl_number_control`.`plant_name` AS `plant_name`,`tbl_number_control`.`kinds` AS `kinds`,`tbl_number_control`.`fields` AS `fields`,`tbl_number_control`.`part_id` AS `part_id`,`tbl_number_control`.`serial_num` AS `serial_num`,`tbl_number_control`.`revisions` AS `revisions`,`tbl_number_control`.`num_no_revisions` AS `num_no_revisions`,`tbl_obs`.`title` AS `title`,`tbl_obs`.`title_trans` AS `title_trans`,`tbl_obs`.`part_trans` AS `part_trans`,`tbl_obs`.`scope` AS `scope`,`tbl_obs`.`scope_trans` AS `scope_trans`,`tbl_obs`.`scope_comment` AS `scope_comment`,`tbl_obs`.`part_trans_scope` AS `part_trans_scope`,`tbl_obs`.`observation_target` AS `observation_target`,`tbl_obs`.`final_ver` AS `final_ver`,`tbl_obs`.`package_exclude` AS `package_exclude`,`tbl_obs`.`val_comp` AS `val_comp`,`tbl_obs`.`general_comment` AS `general_comment`,`tbl_obs`.`trans_range` AS `trans_range`,`tbl_obs`.`trans_deadline` AS `trans_deadline`,`tbl_obs`.`trans_lang` AS `trans_lang`,`tbl_obs`.`trans_state_req` AS `trans_state_req`,`tbl_obs`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_obs`.`trans_req_date` AS `trans_req_date`,`tbl_obs`.`translating_state` AS `translating_state`,`tbl_obs`.`translating_state_id` AS `translating_state_id`,`tbl_obs`.`translated_state` AS `translated_state`,`tbl_obs`.`translated_state_id` AS `translated_state_id`,`tbl_obs`.`approval_state_pr` AS `approval_state_pr`,`tbl_obs`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_obs`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_obs`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_obs`.`approval_state1` AS `approval_state1`,`tbl_obs`.`approval_state1_id` AS `approval_state1_id`,`tbl_obs`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_obs`.`approval_state2` AS `approval_state2`,`tbl_obs`.`approval_state2_id` AS `approval_state2_id`,`tbl_obs`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_obs`.`approval_state3` AS `approval_state3`,`tbl_obs`.`approval_state3_id` AS `approval_state3_id`,`tbl_obs`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_obs`.`approval_state_tl` AS `approval_state_tl`,`tbl_obs`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_obs`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_obs`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_number_control`.`language` AS `language`,`tbl_obs`.`hold` AS `hold`,`tbl_number_control`.`newest_flag` AS `newest_flag`,`tbl_number_control`.`delete_flag` AS `delete_flag`,`tbl_obs`.`editor` AS `editor`,`tbl_obs`.`last_update` AS `last_update`,`tbl_obs`.`last_user` AS `last_user` from (`tbl_obs` join `tbl_number_control` on(((`tbl_obs`.`num` = `tbl_number_control`.`num`) and (`tbl_number_control`.`delete_flag` = `tbl_obs`.`delete_flag`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_obs_extend`
--

/*!50001 DROP VIEW IF EXISTS `vw_obs_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_obs_extend` AS select `obs`.`num` AS `num`,`obs`.`plant_name` AS `plant_name`,`obs`.`kinds` AS `kinds`,`obs`.`fields` AS `fields`,`obs`.`part_id` AS `part_id`,`obs`.`serial_num` AS `serial_num`,`obs`.`revisions` AS `revisions`,`obs`.`num_no_revisions` AS `num_no_revisions`,`obs`.`title` AS `title`,`obs`.`title_trans` AS `title_trans`,`obs`.`part_trans` AS `part_trans`,`obs`.`scope` AS `scope`,`obs`.`scope_trans` AS `scope_trans`,`obs`.`scope_comment` AS `scope_comment`,`obs`.`part_trans_scope` AS `part_trans_scope`,`obs`.`observation_target` AS `observation_target`,`obs`.`final_ver` AS `final_ver`,`obs`.`package_exclude` AS `package_exclude`,`obs`.`val_comp` AS `val_comp`,`obs`.`general_comment` AS `general_comment`,`obs`.`trans_range` AS `trans_range`,`obs`.`trans_deadline` AS `trans_deadline`,`obs`.`trans_lang` AS `trans_lang`,`obs`.`trans_state_req` AS `trans_state_req`,`obs`.`trans_state_req_id` AS `trans_state_req_id`,`obs`.`trans_req_date` AS `trans_req_date`,`obs`.`translating_state` AS `translating_state`,`obs`.`translating_state_id` AS `translating_state_id`,`obs`.`translated_state` AS `translated_state`,`obs`.`translated_state_id` AS `translated_state_id`,`obs`.`approval_state_pr` AS `approval_state_pr`,`obs`.`approval_state_pr_id` AS `approval_state_pr_id`,`obs`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`obs`.`approval_state_pr_state` AS `approval_state_pr_state`,`obs`.`approval_state1` AS `approval_state1`,`obs`.`approval_state1_id` AS `approval_state1_id`,`obs`.`approval_state1_rev` AS `approval_state1_rev`,`obs`.`approval_state2` AS `approval_state2`,`obs`.`approval_state2_id` AS `approval_state2_id`,`obs`.`approval_state2_rev` AS `approval_state2_rev`,`obs`.`approval_state3` AS `approval_state3`,`obs`.`approval_state3_id` AS `approval_state3_id`,`obs`.`approval_state3_rev` AS `approval_state3_rev`,`obs`.`approval_state_tl` AS `approval_state_tl`,`obs`.`approval_state_tl_id` AS `approval_state_tl_id`,`obs`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`obs`.`approval_state_tl_state` AS `approval_state_tl_state`,`obs`.`language` AS `language`,`obs`.`hold` AS `hold`,`obs`.`newest_flag` AS `newest_flag`,`obs`.`delete_flag` AS `delete_flag`,`obs`.`editor` AS `editor`,`obs`.`last_update` AS `last_update`,`obs`.`last_user` AS `last_user`,substr(`obs`.`num`,(locate('-',`obs`.`num`) + 1),(length(`obs`.`num`) - locate('-',`obs`.`num`))) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `obs`.`plant_name`) and (`tbl_field`.`fields` = `obs`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (`tbl_translate`.`num` = `obs`.`num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cancel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (`tbl_translate`.`num` = `obs`.`num`))) limit 1) AS `trans_cancel`,(select `tblobs`.`revisions` from `vw_obs_control` `tblobs` where ((`tblobs`.`plant_name` = `obs`.`plant_name`) and (`tblobs`.`kinds` = `obs`.`kinds`) and (`tblobs`.`fields` = `obs`.`fields`) and (`tblobs`.`part_id` = `obs`.`part_id`) and (`tblobs`.`serial_num` = `obs`.`serial_num`) and (`tblobs`.`revisions` <= `obs`.`revisions`) and (`tblobs`.`delete_flag` = `FN_GET_CONSTANT`('DELETE_FLAG','OFF')) and (`tblobs`.`translated_state` = `FN_GET_CONSTANT`('TRANSLATED_STATE','ON'))) order by `tblobs`.`num` desc limit 1) AS `past_translated_rev`,(select `tblobs`.`translated_state` from `vw_obs_control` `tblobs` where ((`tblobs`.`plant_name` = `obs`.`plant_name`) and (`tblobs`.`kinds` = `obs`.`kinds`) and (`tblobs`.`fields` = `obs`.`fields`) and (`tblobs`.`part_id` = `obs`.`part_id`) and (`tblobs`.`serial_num` = `obs`.`serial_num`) and (`tblobs`.`revisions` <= `obs`.`revisions`) and (`tblobs`.`delete_flag` = `FN_GET_CONSTANT`('DELETE_FLAG','OFF')) and (`tblobs`.`translated_state` = `FN_GET_CONSTANT`('TRANSLATED_STATE','ON'))) order by `tblobs`.`num` desc limit 1) AS `past_translated_status` from `vw_obs_control` `obs` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_obs_extend_filter`
--

/*!50001 DROP VIEW IF EXISTS `vw_obs_extend_filter`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_obs_extend_filter` AS select `vw_obs_extend`.`num` AS `num`,`vw_obs_extend`.`plant_name` AS `plant_name`,`vw_obs_extend`.`kinds` AS `kinds`,`vw_obs_extend`.`fields` AS `fields`,`vw_obs_extend`.`part_id` AS `part_id`,`vw_obs_extend`.`serial_num` AS `serial_num`,`vw_obs_extend`.`revisions` AS `revisions`,`vw_obs_extend`.`num_no_revisions` AS `num_no_revisions`,`vw_obs_extend`.`title` AS `title`,`vw_obs_extend`.`title_trans` AS `title_trans`,`vw_obs_extend`.`part_trans` AS `part_trans`,`vw_obs_extend`.`scope` AS `scope`,`vw_obs_extend`.`scope_trans` AS `scope_trans`,`vw_obs_extend`.`scope_comment` AS `scope_comment`,`vw_obs_extend`.`part_trans_scope` AS `part_trans_scope`,`vw_obs_extend`.`observation_target` AS `observation_target`,`vw_obs_extend`.`final_ver` AS `final_ver`,`vw_obs_extend`.`package_exclude` AS `package_exclude`,`vw_obs_extend`.`val_comp` AS `val_comp`,`vw_obs_extend`.`general_comment` AS `general_comment`,`vw_obs_extend`.`trans_range` AS `trans_range`,`vw_obs_extend`.`trans_deadline` AS `trans_deadline`,`vw_obs_extend`.`trans_lang` AS `trans_lang`,`vw_obs_extend`.`trans_state_req` AS `trans_state_req`,`vw_obs_extend`.`trans_state_req_id` AS `trans_state_req_id`,`vw_obs_extend`.`trans_req_date` AS `trans_req_date`,`vw_obs_extend`.`translating_state` AS `translating_state`,`vw_obs_extend`.`translating_state_id` AS `translating_state_id`,`vw_obs_extend`.`translated_state` AS `translated_state`,`vw_obs_extend`.`translated_state_id` AS `translated_state_id`,`vw_obs_extend`.`approval_state_pr` AS `approval_state_pr`,`vw_obs_extend`.`approval_state_pr_id` AS `approval_state_pr_id`,`vw_obs_extend`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`vw_obs_extend`.`approval_state_pr_state` AS `approval_state_pr_state`,`vw_obs_extend`.`approval_state1` AS `approval_state1`,`vw_obs_extend`.`approval_state1_id` AS `approval_state1_id`,`vw_obs_extend`.`approval_state1_rev` AS `approval_state1_rev`,`vw_obs_extend`.`approval_state2` AS `approval_state2`,`vw_obs_extend`.`approval_state2_id` AS `approval_state2_id`,`vw_obs_extend`.`approval_state2_rev` AS `approval_state2_rev`,`vw_obs_extend`.`approval_state3` AS `approval_state3`,`vw_obs_extend`.`approval_state3_id` AS `approval_state3_id`,`vw_obs_extend`.`approval_state3_rev` AS `approval_state3_rev`,`vw_obs_extend`.`approval_state_tl` AS `approval_state_tl`,`vw_obs_extend`.`approval_state_tl_id` AS `approval_state_tl_id`,`vw_obs_extend`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`vw_obs_extend`.`approval_state_tl_state` AS `approval_state_tl_state`,`vw_obs_extend`.`language` AS `language`,`vw_obs_extend`.`hold` AS `hold`,`vw_obs_extend`.`newest_flag` AS `newest_flag`,`vw_obs_extend`.`delete_flag` AS `delete_flag`,`vw_obs_extend`.`editor` AS `editor`,`vw_obs_extend`.`last_update` AS `last_update`,`vw_obs_extend`.`last_user` AS `last_user`,`vw_obs_extend`.`l_num` AS `l_num`,`vw_obs_extend`.`field_order` AS `field_order`,`vw_obs_extend`.`trans_situation` AS `trans_situation`,`vw_obs_extend`.`trans_cancel` AS `trans_cancel`,`vw_obs_extend`.`past_translated_rev` AS `past_translated_rev`,`vw_obs_extend`.`past_translated_status` AS `past_translated_status` from `vw_obs_extend` where ((`vw_obs_extend`.`newest_flag` <> `FN_GET_CONSTANT`('NEWEST_FLAG','OFF')) and (`vw_obs_extend`.`delete_flag` = `FN_GET_CONSTANT`('DELETE_FLAG','OFF')) and (`vw_obs_extend`.`hold` <> `FN_GET_CONSTANT`('HOLD_FLAG','OFF'))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_obs_extend_refer`
--

/*!50001 DROP VIEW IF EXISTS `vw_obs_extend_refer`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_obs_extend_refer` AS select `vw_obs_control`.`num` AS `num`,`vw_obs_control`.`plant_name` AS `plant_name`,`vw_obs_control`.`kinds` AS `kinds`,`vw_obs_control`.`fields` AS `fields`,`vw_obs_control`.`part_id` AS `part_id`,`vw_obs_control`.`serial_num` AS `serial_num`,`vw_obs_control`.`revisions` AS `revisions`,`vw_obs_control`.`title` AS `title`,`vw_obs_control`.`title_trans` AS `title_trans`,`vw_obs_control`.`part_trans` AS `part_trans`,`vw_obs_control`.`scope` AS `scope`,`vw_obs_control`.`scope_trans` AS `scope_trans`,`vw_obs_control`.`scope_comment` AS `scope_comment`,`vw_obs_control`.`part_trans_scope` AS `part_trans_scope`,`vw_obs_control`.`observation_target` AS `observation_target`,`vw_obs_control`.`final_ver` AS `final_ver`,`vw_obs_control`.`package_exclude` AS `package_exclude`,`vw_obs_control`.`val_comp` AS `val_comp`,`vw_obs_control`.`general_comment` AS `general_comment`,`vw_obs_control`.`trans_range` AS `trans_range`,`vw_obs_control`.`trans_deadline` AS `trans_deadline`,`vw_obs_control`.`trans_lang` AS `trans_lang`,`vw_obs_control`.`trans_state_req` AS `trans_state_req`,`vw_obs_control`.`trans_state_req_id` AS `trans_state_req_id`,`vw_obs_control`.`trans_req_date` AS `trans_req_date`,`vw_obs_control`.`translating_state` AS `translating_state`,`vw_obs_control`.`translating_state_id` AS `translating_state_id`,`vw_obs_control`.`translated_state` AS `translated_state`,`vw_obs_control`.`translated_state_id` AS `translated_state_id`,`vw_obs_control`.`approval_state_pr` AS `approval_state_pr`,`vw_obs_control`.`approval_state_pr_id` AS `approval_state_pr_id`,`vw_obs_control`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`vw_obs_control`.`approval_state_pr_state` AS `approval_state_pr_state`,`vw_obs_control`.`approval_state1` AS `approval_state1`,`vw_obs_control`.`approval_state1_id` AS `approval_state1_id`,`vw_obs_control`.`approval_state1_rev` AS `approval_state1_rev`,`vw_obs_control`.`approval_state2` AS `approval_state2`,`vw_obs_control`.`approval_state2_id` AS `approval_state2_id`,`vw_obs_control`.`approval_state2_rev` AS `approval_state2_rev`,`vw_obs_control`.`approval_state3` AS `approval_state3`,`vw_obs_control`.`approval_state3_id` AS `approval_state3_id`,`vw_obs_control`.`approval_state3_rev` AS `approval_state3_rev`,`vw_obs_control`.`approval_state_tl` AS `approval_state_tl`,`vw_obs_control`.`approval_state_tl_id` AS `approval_state_tl_id`,`vw_obs_control`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`vw_obs_control`.`approval_state_tl_state` AS `approval_state_tl_state`,`vw_obs_control`.`language` AS `language`,`vw_obs_control`.`hold` AS `hold`,`vw_obs_control`.`newest_flag` AS `newest_flag`,`vw_obs_control`.`delete_flag` AS `delete_flag`,`vw_obs_control`.`editor` AS `editor`,`vw_obs_control`.`last_update` AS `last_update`,`vw_obs_control`.`last_user` AS `last_user`,if((`vw_obs_control`.`approval_state_pr` = true),concat('【レビュワ】',if((`vw_obs_control`.`approval_state_pr_state` is null),'',`vw_obs_control`.`approval_state_pr_state`)),concat(if((`vw_obs_control`.`approval_state1` = true),'【審1】',''),if((`vw_obs_control`.`approval_state2` = true),'【審2】',''),if((`vw_obs_control`.`approval_state3` = true),'【審3】',''),if((`vw_obs_control`.`approval_state_tl` = true),'【TL】',''),if((`vw_obs_control`.`approval_state_tl_state` is null),'',if((`vw_obs_control`.`approval_state_tl_state` is null),'',`vw_obs_control`.`approval_state_tl_state`)))) AS `approval_status_tl_jpa`,if((`vw_obs_control`.`approval_state_pr` = true),concat('【Reviewer】',if((`vw_obs_control`.`approval_state_pr_state` is null),'',`vw_obs_control`.`approval_state_pr_state`)),concat(if((`vw_obs_control`.`approval_state1` = true),'【As.1】',''),if((`vw_obs_control`.`approval_state2` = true),'【As.2】',''),if((`vw_obs_control`.`approval_state3` = true),'【As.3】',''),if((`vw_obs_control`.`approval_state_tl` = true),'【TL】',''),if((`vw_obs_control`.`approval_state_tl_state` is null),'',if((`vw_obs_control`.`approval_state_tl_state` is null),'',`vw_obs_control`.`approval_state_tl_state`)))) AS `approval_status_tl_eng`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `vw_obs_control`.`plant_name`) and (`tbl_field`.`fields` = `vw_obs_control`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (`tbl_translate`.`num` = `tbl_translate`.`num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cancel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (`tbl_translate`.`num` = `tbl_translate`.`num`))) limit 1) AS `trans_cancel`,(select `vw_obs_control`.`revisions` from `vw_obs_control` `tblobs` where ((`tblobs`.`num` = `vw_obs_control`.`num`) and (`tblobs`.`revisions` <= `vw_obs_control`.`revisions`) and (`tblobs`.`delete_flag` = false) and (`tblobs`.`translated_state` = true)) order by `tblobs`.`num` desc limit 1) AS `past_translated_rev`,(select `vw_obs_control`.`translated_state` from `vw_obs_control` `tblobs` where ((`tblobs`.`num` = `vw_obs_control`.`num`) and (`tblobs`.`revisions` <= `vw_obs_control`.`revisions`) and (`tblobs`.`delete_flag` = false) and (`tblobs`.`translated_state` = true)) order by `tblobs`.`num` desc limit 1) AS `past_translated_status` from `vw_obs_control` where (`vw_obs_control`.`delete_flag` = false) order by `vw_obs_control`.`num` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_obs_fact_attach`
--

/*!50001 DROP VIEW IF EXISTS `vw_obs_fact_attach`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_obs_fact_attach` AS select `f`.`num` AS `num`,`f`.`fact` AS `fact`,`f`.`fact_trans` AS `fact_trans`,`f`.`comment` AS `comment`,`f`.`part_trans` AS `part_trans`,`f`.`plus_neutral_delta` AS `plus_neutral_delta`,`f`.`val_comp` AS `val_comp`,`f`.`offer_field` AS `offer_field`,`f`.`poc` AS `poc`,`f`.`safety_culture` AS `safety_culture`,`a`.`serial_num` AS `serial_num`,`a`.`num_no_revisions` AS `num_no_revisions`,`f`.`fact_num` AS `fact_num`,`a`.`fact_num` AS `attach_fact_num`,`a`.`represent_photo_flag` AS `represent_photo_flag`,`a`.`file_name` AS `file_name`,`a`.`size` AS `size`,`a`.`delete_flag` AS `delete_flag`,`f`.`display_order` AS `display_order`,`f`.`last_update` AS `last_update`,`f`.`last_user` AS `last_user` from ((`tbl_obs_fact` `f` join `tbl_number_control` `nc` on((`f`.`num` = `nc`.`num`))) left join `tbl_obs_attach` `a` on((`a`.`num_no_revisions` = `nc`.`num_no_revisions`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_translate_control`
--

/*!50001 DROP VIEW IF EXISTS `vw_translate_control`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_translate_control` AS select `tbl_number_control`.`plant_name` AS `plant_name`,`tbl_number_control`.`kinds` AS `kinds`,`tbl_number_control`.`fields` AS `fields`,`tbl_number_control`.`part_id` AS `part_id`,`tbl_number_control`.`serial_num` AS `serial_num`,`tbl_number_control`.`revisions` AS `revisions`,`tbl_number_control`.`language` AS `language`,`tbl_translate`.`num` AS `num`,`tbl_translate`.`offer_num` AS `offer_num`,`tbl_translate`.`title` AS `title`,`tbl_translate`.`title_trans` AS `title_trans`,`tbl_translate`.`ver_trans_done` AS `ver_trans_done`,`tbl_translate`.`ver_original` AS `ver_original`,`tbl_translate`.`editor` AS `editor`,`tbl_translate`.`trans_situation` AS `trans_situation`,`tbl_translate`.`trans_arrival` AS `trans_arrival`,`tbl_translate`.`trans_cancel` AS `trans_cancel`,`tbl_translate`.`trans_urgent` AS `trans_urgent`,`tbl_translate`.`contact_memo` AS `contact_memo`,`tbl_translate`.`output_date` AS `output_date`,`tbl_translate`.`capture_date` AS `capture_date`,`tbl_translate`.`trans_date` AS `trans_date`,`tbl_translate`.`translator` AS `translator`,`tbl_translate`.`cancel_status` AS `cancel_status`,`tbl_translate`.`trans_scope` AS `trans_scope`,`tbl_translate`.`trans_deadline` AS `trans_deadline`,`tbl_translate`.`trans_lang` AS `trans_lang`,`tbl_translate`.`trans_req_date` AS `trans_req_date`,`tbl_translate`.`file_name` AS `file_name`,`tbl_translate`.`select_check` AS `select_check`,`tbl_translate`.`last_update` AS `last_update`,`tbl_translate`.`last_user` AS `last_user` from (`tbl_translate` join `tbl_number_control` on((`tbl_number_control`.`num` = `tbl_translate`.`num`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-08-25 17:13:10
