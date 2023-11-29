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
-- Temporary view structure for view `tbl_pfa_extend`
--

DROP TABLE IF EXISTS `tbl_pfa_extend`;
/*!50001 DROP VIEW IF EXISTS `tbl_pfa_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tbl_pfa_extend` AS SELECT 
 1 AS `id`,
 1 AS `plant_name`,
 1 AS `years`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `branch_num`,
 1 AS `revisions`,
 1 AS `achievement_goal`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `val_comp`,
 1 AS `general_jdc`,
 1 AS `final_ver`,
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
 1 AS `english_edition`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `new_num`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cansel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `tbl_pd_extend`
--

DROP TABLE IF EXISTS `tbl_pd_extend`;
/*!50001 DROP VIEW IF EXISTS `tbl_pd_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tbl_pd_extend` AS SELECT 
 1 AS `id`,
 1 AS `plant_name`,
 1 AS `years`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `serial_num`,
 1 AS `revisions`,
 1 AS `achievement_goal`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `val_comp`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `title_part_trans`,
 1 AS `explan_text`,
 1 AS `explan_text_trans`,
 1 AS `part_trans`,
 1 AS `explan_text_translation`,
 1 AS `explan_text_jude`,
 1 AS `explan_text_jdc`,
 1 AS `general_jdc`,
 1 AS `connect_num`,
 1 AS `connect_memo`,
 1 AS `connect_memo_trans`,
 1 AS `connect_memo_parttrans`,
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
 1 AS `english_edition`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `new_num`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cansel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `tbl_gfa_extend`
--

DROP TABLE IF EXISTS `tbl_gfa_extend`;
/*!50001 DROP VIEW IF EXISTS `tbl_gfa_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tbl_gfa_extend` AS SELECT 
 1 AS `id`,
 1 AS `plant_name`,
 1 AS `years`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `branch_num`,
 1 AS `revisions`,
 1 AS `achievement_goal`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `val_comp`,
 1 AS `general_jdc`,
 1 AS `final_ver`,
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
 1 AS `english_edition`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `new_num`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cansel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `tbl_obs_extend`
--

DROP TABLE IF EXISTS `tbl_obs_extend`;
/*!50001 DROP VIEW IF EXISTS `tbl_obs_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tbl_obs_extend` AS SELECT 
 1 AS `id`,
 1 AS `plant_name`,
 1 AS `years`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `serial_num`,
 1 AS `revisions`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `observation_target`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `val_comp`,
 1 AS `general_jdc`,
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
 1 AS `english_edition`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `new_num`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cansel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `tbl_afi_extend`
--

DROP TABLE IF EXISTS `tbl_afi_extend`;
/*!50001 DROP VIEW IF EXISTS `tbl_afi_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tbl_afi_extend` AS SELECT 
 1 AS `id`,
 1 AS `plant_name`,
 1 AS `years`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `serial_num`,
 1 AS `revisions`,
 1 AS `achievement_goal`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `observation_target`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `val_comp`,
 1 AS `general_jdc`,
 1 AS `connect_num`,
 1 AS `connect_memo`,
 1 AS `connect_memo_trans`,
 1 AS `connect_memo_parttrans`,
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
 1 AS `english_edition`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `new_num`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cansel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `tbl_bp_extend`
--

DROP TABLE IF EXISTS `tbl_bp_extend`;
/*!50001 DROP VIEW IF EXISTS `tbl_bp_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tbl_bp_extend` AS SELECT 
 1 AS `id`,
 1 AS `plant_name`,
 1 AS `years`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `branch_num`,
 1 AS `revisions`,
 1 AS `achievement_goal`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `val_comp`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `title_part_trans`,
 1 AS `explan_text`,
 1 AS `explan_text_trans`,
 1 AS `part_trans`,
 1 AS `part_trans_explan`,
 1 AS `explan_text_translation`,
 1 AS `explan_text_jdc`,
 1 AS `general_jdc`,
 1 AS `connect_num`,
 1 AS `connect_memo`,
 1 AS `connect_memo_trans`,
 1 AS `connect_memo_parttrans`,
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
 1 AS `english_edition`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `new_num`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cansel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `tbl_str_extend`
--

DROP TABLE IF EXISTS `tbl_str_extend`;
/*!50001 DROP VIEW IF EXISTS `tbl_str_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tbl_str_extend` AS SELECT 
 1 AS `id`,
 1 AS `plant_name`,
 1 AS `years`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `branch_num`,
 1 AS `revisions`,
 1 AS `achievement_goal`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `val_comp`,
 1 AS `general_jdc`,
 1 AS `final_ver`,
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
 1 AS `english_edition`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `new_num`,
 1 AS `l_num`,
 1 AS `FieldOrder`,
 1 AS `trans_situation`,
 1 AS `trans_cansel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `tbl_soi_extend`
--

DROP TABLE IF EXISTS `tbl_soi_extend`;
/*!50001 DROP VIEW IF EXISTS `tbl_soi_extend`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `tbl_soi_extend` AS SELECT 
 1 AS `id`,
 1 AS `plant_name`,
 1 AS `years`,
 1 AS `kinds`,
 1 AS `fields`,
 1 AS `part_id`,
 1 AS `branch_num`,
 1 AS `revisions`,
 1 AS `achievement_goal`,
 1 AS `title`,
 1 AS `title_trans`,
 1 AS `part_trans`,
 1 AS `final_ver`,
 1 AS `package_exclude`,
 1 AS `follow_up_num`,
 1 AS `general_jdc`,
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
 1 AS `english_edition`,
 1 AS `hold`,
 1 AS `newest_flag`,
 1 AS `delete_flag`,
 1 AS `editor`,
 1 AS `last_update`,
 1 AS `last_user`,
 1 AS `new_num`,
 1 AS `l_num`,
 1 AS `field_order`,
 1 AS `trans_situation`,
 1 AS `trans_cansel`,
 1 AS `past_translated_rev`,
 1 AS `past_translated_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `tbl_pfa_extend`
--

/*!50001 DROP VIEW IF EXISTS `tbl_pfa_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tbl_pfa_extend` AS select `tbl_pfa`.`id` AS `id`,`tbl_pfa`.`plant_name` AS `plant_name`,`tbl_pfa`.`years` AS `years`,`tbl_pfa`.`kinds` AS `kinds`,`tbl_pfa`.`fields` AS `fields`,`tbl_pfa`.`part_id` AS `part_id`,`tbl_pfa`.`branch_num` AS `branch_num`,`tbl_pfa`.`revisions` AS `revisions`,`tbl_pfa`.`achievement_goal` AS `achievement_goal`,`tbl_pfa`.`title` AS `title`,`tbl_pfa`.`title_trans` AS `title_trans`,`tbl_pfa`.`part_trans` AS `part_trans`,`tbl_pfa`.`val_comp` AS `val_comp`,`tbl_pfa`.`general_jdc` AS `general_jdc`,`tbl_pfa`.`final_ver` AS `final_ver`,`tbl_pfa`.`trans_range` AS `trans_range`,`tbl_pfa`.`trans_deadline` AS `trans_deadline`,`tbl_pfa`.`trans_lang` AS `trans_lang`,`tbl_pfa`.`trans_state_req` AS `trans_state_req`,`tbl_pfa`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_pfa`.`trans_req_date` AS `trans_req_date`,`tbl_pfa`.`translating_state` AS `translating_state`,`tbl_pfa`.`translating_state_id` AS `translating_state_id`,`tbl_pfa`.`translated_state` AS `translated_state`,`tbl_pfa`.`translated_state_id` AS `translated_state_id`,`tbl_pfa`.`approval_state_pr` AS `approval_state_pr`,`tbl_pfa`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_pfa`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_pfa`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_pfa`.`approval_state1` AS `approval_state1`,`tbl_pfa`.`approval_state1_id` AS `approval_state1_id`,`tbl_pfa`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_pfa`.`approval_state2` AS `approval_state2`,`tbl_pfa`.`approval_state2_id` AS `approval_state2_id`,`tbl_pfa`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_pfa`.`approval_state3` AS `approval_state3`,`tbl_pfa`.`approval_state3_id` AS `approval_state3_id`,`tbl_pfa`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_pfa`.`approval_state_tl` AS `approval_state_tl`,`tbl_pfa`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_pfa`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_pfa`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_pfa`.`english_edition` AS `english_edition`,`tbl_pfa`.`hold` AS `hold`,`tbl_pfa`.`newest_flag` AS `newest_flag`,`tbl_pfa`.`delete_flag` AS `delete_flag`,`tbl_pfa`.`editor` AS `editor`,`tbl_pfa`.`last_update` AS `last_update`,`tbl_pfa`.`last_user` AS `last_user`,concat(`tbl_pfa`.`plant_name`,'-',`tbl_pfa`.`kinds`,'-',`tbl_pfa`.`fields`,'-',`tbl_pfa`.`part_id`,'-',convert(lpad(`tbl_pfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_pfa`.`revisions`,if((`tbl_pfa`.`english_edition` = 'E'),'-E','')) AS `new_num`,concat(`tbl_pfa`.`kinds`,'-',`tbl_pfa`.`fields`,'-',`tbl_pfa`.`part_id`,'-',convert(lpad(`tbl_pfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_pfa`.`revisions`,if((`tbl_pfa`.`english_edition` = 'E'),'-E','')) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `tbl_pfa`.`plant_name`) and (`tbl_field`.`fields` = `tbl_pfa`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cansel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_cansel`,(select `tbl_pfa`.`revisions` from `tbl_pfa` `tblpfa` where ((`tblpfa`.`plant_name` = `tbl_pfa`.`plant_name`) and (`tblpfa`.`kinds` = `tbl_pfa`.`kinds`) and (`tblpfa`.`fields` = `tbl_pfa`.`fields`) and (`tblpfa`.`part_id` = `tbl_pfa`.`part_id`) and (`tblpfa`.`branch_num` = `tbl_pfa`.`branch_num`) and (`tblpfa`.`revisions` <= `tbl_pfa`.`revisions`) and (`tblpfa`.`delete_flag` = 0) and (`tblpfa`.`translated_state` = 1)) order by concat(`tblpfa`.`plant_name`,'-',`tblpfa`.`kinds`,'-',`tblpfa`.`fields`,'-',`tblpfa`.`part_id`,'-',convert(lpad(`tbl_pfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tblpfa`.`revisions`,if((`tblpfa`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_rev`,(select `tbl_pfa`.`translated_state` from `tbl_pfa` `tblpfa` where ((`tblpfa`.`plant_name` = `tbl_pfa`.`plant_name`) and (`tblpfa`.`kinds` = `tbl_pfa`.`kinds`) and (`tblpfa`.`fields` = `tbl_pfa`.`fields`) and (`tblpfa`.`part_id` = `tbl_pfa`.`part_id`) and (`tblpfa`.`branch_num` = `tbl_pfa`.`branch_num`) and (`tblpfa`.`revisions` <= `tbl_pfa`.`revisions`) and (`tblpfa`.`delete_flag` = 0) and (`tblpfa`.`translated_state` = 1)) order by concat(`tblpfa`.`plant_name`,'-',`tblpfa`.`kinds`,'-',`tblpfa`.`fields`,'-',`tblpfa`.`part_id`,'-',convert(lpad(`tbl_pfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tblpfa`.`revisions`,if((`tblpfa`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_status` from `tbl_pfa` where ((`tbl_pfa`.`newest_flag` = -(1)) and (`tbl_pfa`.`delete_flag` = 0) and (`tbl_pfa`.`hold` = -(1))) order by concat(`tbl_pfa`.`plant_name`,'-',`tbl_pfa`.`kinds`,'-',`tbl_pfa`.`fields`,'-',`tbl_pfa`.`part_id`,'-',convert(lpad(`tbl_pfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_pfa`.`revisions`,if((`tbl_pfa`.`english_edition` = 'E'),'-E','')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `tbl_pd_extend`
--

/*!50001 DROP VIEW IF EXISTS `tbl_pd_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tbl_pd_extend` AS select `tbl_pd`.`id` AS `id`,`tbl_pd`.`plant_name` AS `plant_name`,`tbl_pd`.`years` AS `years`,`tbl_pd`.`kinds` AS `kinds`,`tbl_pd`.`fields` AS `fields`,`tbl_pd`.`part_id` AS `part_id`,`tbl_pd`.`serial_num` AS `serial_num`,`tbl_pd`.`revisions` AS `revisions`,`tbl_pd`.`achievement_goal` AS `achievement_goal`,`tbl_pd`.`final_ver` AS `final_ver`,`tbl_pd`.`package_exclude` AS `package_exclude`,`tbl_pd`.`val_comp` AS `val_comp`,`tbl_pd`.`title` AS `title`,`tbl_pd`.`title_trans` AS `title_trans`,`tbl_pd`.`title_part_trans` AS `title_part_trans`,`tbl_pd`.`explan_text` AS `explan_text`,`tbl_pd`.`explan_text_trans` AS `explan_text_trans`,`tbl_pd`.`part_trans` AS `part_trans`,`tbl_pd`.`explan_text_translation` AS `explan_text_translation`,`tbl_pd`.`explan_text_jude` AS `explan_text_jude`,`tbl_pd`.`explan_text_jdc` AS `explan_text_jdc`,`tbl_pd`.`general_jdc` AS `general_jdc`,`tbl_pd`.`connect_num` AS `connect_num`,`tbl_pd`.`connect_memo` AS `connect_memo`,`tbl_pd`.`connect_memo_trans` AS `connect_memo_trans`,`tbl_pd`.`connect_memo_parttrans` AS `connect_memo_parttrans`,`tbl_pd`.`trans_range` AS `trans_range`,`tbl_pd`.`trans_deadline` AS `trans_deadline`,`tbl_pd`.`trans_lang` AS `trans_lang`,`tbl_pd`.`trans_state_req` AS `trans_state_req`,`tbl_pd`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_pd`.`trans_req_date` AS `trans_req_date`,`tbl_pd`.`translating_state` AS `translating_state`,`tbl_pd`.`translating_state_id` AS `translating_state_id`,`tbl_pd`.`translated_state` AS `translated_state`,`tbl_pd`.`translated_state_id` AS `translated_state_id`,`tbl_pd`.`approval_state_pr` AS `approval_state_pr`,`tbl_pd`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_pd`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_pd`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_pd`.`approval_state1` AS `approval_state1`,`tbl_pd`.`approval_state1_id` AS `approval_state1_id`,`tbl_pd`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_pd`.`approval_state2` AS `approval_state2`,`tbl_pd`.`approval_state2_id` AS `approval_state2_id`,`tbl_pd`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_pd`.`approval_state3` AS `approval_state3`,`tbl_pd`.`approval_state3_id` AS `approval_state3_id`,`tbl_pd`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_pd`.`approval_state_tl` AS `approval_state_tl`,`tbl_pd`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_pd`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_pd`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_pd`.`english_edition` AS `english_edition`,`tbl_pd`.`hold` AS `hold`,`tbl_pd`.`newest_flag` AS `newest_flag`,`tbl_pd`.`delete_flag` AS `delete_flag`,`tbl_pd`.`editor` AS `editor`,`tbl_pd`.`last_update` AS `last_update`,`tbl_pd`.`last_user` AS `last_user`,concat(`tbl_pd`.`plant_name`,'-',`tbl_pd`.`kinds`,'-',`tbl_pd`.`fields`,'-',`tbl_pd`.`part_id`,'-',convert(lpad(`tbl_pd`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_pd`.`revisions`,if((`tbl_pd`.`english_edition` = 'E'),'-E','')) AS `new_num`,concat(`tbl_pd`.`kinds`,'-',`tbl_pd`.`fields`,'-',`tbl_pd`.`part_id`,'-',convert(lpad(`tbl_pd`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_pd`.`revisions`,if((`tbl_pd`.`english_edition` = 'E'),'-E','')) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `tbl_pd`.`plant_name`) and (`tbl_field`.`fields` = `tbl_pd`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cansel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_cansel`,(select `tbl_pd`.`revisions` from `tbl_pd` `tblpd` where ((`tblpd`.`plant_name` = `tbl_pd`.`plant_name`) and (`tblpd`.`kinds` = `tbl_pd`.`kinds`) and (`tblpd`.`fields` = `tbl_pd`.`fields`) and (`tblpd`.`part_id` = `tbl_pd`.`part_id`) and (`tblpd`.`serial_num` = `tbl_pd`.`serial_num`) and (`tblpd`.`revisions` <= `tbl_pd`.`revisions`) and (`tblpd`.`delete_flag` = 0) and (`tblpd`.`translated_state` = 1)) order by concat(`tblpd`.`plant_name`,'-',`tblpd`.`kinds`,'-',`tblpd`.`fields`,'-',`tblpd`.`part_id`,'-',convert(lpad(`tbl_pd`.`serial_num`,2,'0') using utf8mb4),'-r',`tblpd`.`revisions`,if((`tblpd`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_rev`,(select `tbl_pd`.`translated_state` from `tbl_pd` `tblpd` where ((`tblpd`.`plant_name` = `tbl_pd`.`plant_name`) and (`tblpd`.`kinds` = `tbl_pd`.`kinds`) and (`tblpd`.`fields` = `tbl_pd`.`fields`) and (`tblpd`.`part_id` = `tbl_pd`.`part_id`) and (`tblpd`.`serial_num` = `tbl_pd`.`serial_num`) and (`tblpd`.`revisions` <= `tbl_pd`.`revisions`) and (`tblpd`.`delete_flag` = 0) and (`tblpd`.`translated_state` = 1)) order by concat(`tblpd`.`plant_name`,'-',`tblpd`.`kinds`,'-',`tblpd`.`fields`,'-',`tblpd`.`part_id`,'-',convert(lpad(`tbl_pd`.`serial_num`,2,'0') using utf8mb4),'-r',`tblpd`.`revisions`,if((`tblpd`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_status` from `tbl_pd` where ((`tbl_pd`.`newest_flag` = -(1)) and (`tbl_pd`.`delete_flag` = 0) and (`tbl_pd`.`hold` = -(1))) order by concat(`tbl_pd`.`plant_name`,'-',`tbl_pd`.`kinds`,'-',`tbl_pd`.`fields`,'-',`tbl_pd`.`part_id`,'-',convert(lpad(`tbl_pd`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_pd`.`revisions`,if((`tbl_pd`.`english_edition` = 'E'),'-E','')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `tbl_gfa_extend`
--

/*!50001 DROP VIEW IF EXISTS `tbl_gfa_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tbl_gfa_extend` AS select `tbl_gfa`.`id` AS `id`,`tbl_gfa`.`plant_name` AS `plant_name`,`tbl_gfa`.`years` AS `years`,`tbl_gfa`.`kinds` AS `kinds`,`tbl_gfa`.`fields` AS `fields`,`tbl_gfa`.`part_id` AS `part_id`,`tbl_gfa`.`branch_num` AS `branch_num`,`tbl_gfa`.`revisions` AS `revisions`,`tbl_gfa`.`achievement_goal` AS `achievement_goal`,`tbl_gfa`.`title` AS `title`,`tbl_gfa`.`title_trans` AS `title_trans`,`tbl_gfa`.`part_trans` AS `part_trans`,`tbl_gfa`.`val_comp` AS `val_comp`,`tbl_gfa`.`general_jdc` AS `general_jdc`,`tbl_gfa`.`final_ver` AS `final_ver`,`tbl_gfa`.`trans_range` AS `trans_range`,`tbl_gfa`.`trans_deadline` AS `trans_deadline`,`tbl_gfa`.`trans_lang` AS `trans_lang`,`tbl_gfa`.`trans_state_req` AS `trans_state_req`,`tbl_gfa`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_gfa`.`trans_req_date` AS `trans_req_date`,`tbl_gfa`.`translating_state` AS `translating_state`,`tbl_gfa`.`translating_state_id` AS `translating_state_id`,`tbl_gfa`.`translated_state` AS `translated_state`,`tbl_gfa`.`translated_state_id` AS `translated_state_id`,`tbl_gfa`.`approval_state_pr` AS `approval_state_pr`,`tbl_gfa`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_gfa`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_gfa`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_gfa`.`approval_state1` AS `approval_state1`,`tbl_gfa`.`approval_state1_id` AS `approval_state1_id`,`tbl_gfa`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_gfa`.`approval_state2` AS `approval_state2`,`tbl_gfa`.`approval_state2_id` AS `approval_state2_id`,`tbl_gfa`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_gfa`.`approval_state3` AS `approval_state3`,`tbl_gfa`.`approval_state3_id` AS `approval_state3_id`,`tbl_gfa`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_gfa`.`approval_state_tl` AS `approval_state_tl`,`tbl_gfa`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_gfa`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_gfa`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_gfa`.`english_edition` AS `english_edition`,`tbl_gfa`.`hold` AS `hold`,`tbl_gfa`.`newest_flag` AS `newest_flag`,`tbl_gfa`.`delete_flag` AS `delete_flag`,`tbl_gfa`.`editor` AS `editor`,`tbl_gfa`.`last_update` AS `last_update`,`tbl_gfa`.`last_user` AS `last_user`,concat(`tbl_gfa`.`plant_name`,'-',`tbl_gfa`.`kinds`,'-',`tbl_gfa`.`fields`,'-',`tbl_gfa`.`part_id`,'-',convert(lpad(`tbl_gfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_gfa`.`revisions`,if((`tbl_gfa`.`english_edition` = 'E'),'-E','')) AS `new_num`,concat(`tbl_gfa`.`kinds`,'-',`tbl_gfa`.`fields`,'-',`tbl_gfa`.`part_id`,'-',convert(lpad(`tbl_gfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_gfa`.`revisions`,if((`tbl_gfa`.`english_edition` = 'E'),'-E','')) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `tbl_gfa`.`plant_name`) and (`tbl_field`.`fields` = `tbl_gfa`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cansel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_cansel`,(select `tbl_gfa`.`revisions` from `tbl_gfa` `tblgfa` where ((`tblgfa`.`plant_name` = `tbl_gfa`.`plant_name`) and (`tblgfa`.`kinds` = `tbl_gfa`.`kinds`) and (`tblgfa`.`fields` = `tbl_gfa`.`fields`) and (`tblgfa`.`part_id` = `tbl_gfa`.`part_id`) and (`tblgfa`.`branch_num` = `tbl_gfa`.`branch_num`) and (`tblgfa`.`revisions` <= `tbl_gfa`.`revisions`) and (`tblgfa`.`delete_flag` = 0) and (`tblgfa`.`translated_state` = 1)) order by concat(`tblgfa`.`plant_name`,'-',`tblgfa`.`kinds`,'-',`tblgfa`.`fields`,'-',`tblgfa`.`part_id`,'-',convert(lpad(`tbl_gfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tblgfa`.`revisions`,if((`tblgfa`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_rev`,(select `tbl_gfa`.`translated_state` from `tbl_gfa` `tblgfa` where ((`tblgfa`.`plant_name` = `tbl_gfa`.`plant_name`) and (`tblgfa`.`kinds` = `tbl_gfa`.`kinds`) and (`tblgfa`.`fields` = `tbl_gfa`.`fields`) and (`tblgfa`.`part_id` = `tbl_gfa`.`part_id`) and (`tblgfa`.`branch_num` = `tbl_gfa`.`branch_num`) and (`tblgfa`.`revisions` <= `tbl_gfa`.`revisions`) and (`tblgfa`.`delete_flag` = 0) and (`tblgfa`.`translated_state` = 1)) order by concat(`tblgfa`.`plant_name`,'-',`tblgfa`.`kinds`,'-',`tblgfa`.`fields`,'-',`tblgfa`.`part_id`,'-',convert(lpad(`tbl_gfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tblgfa`.`revisions`,if((`tblgfa`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_status` from `tbl_gfa` where ((`tbl_gfa`.`newest_flag` = -(1)) and (`tbl_gfa`.`delete_flag` = 0) and (`tbl_gfa`.`hold` = -(1))) order by concat(`tbl_gfa`.`plant_name`,'-',`tbl_gfa`.`kinds`,'-',`tbl_gfa`.`fields`,'-',`tbl_gfa`.`part_id`,'-',convert(lpad(`tbl_gfa`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_gfa`.`revisions`,if((`tbl_gfa`.`english_edition` = 'E'),'-E','')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `tbl_obs_extend`
--

/*!50001 DROP VIEW IF EXISTS `tbl_obs_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tbl_obs_extend` AS select `tbl_obs`.`id` AS `id`,`tbl_obs`.`plant_name` AS `plant_name`,`tbl_obs`.`years` AS `years`,`tbl_obs`.`kinds` AS `kinds`,`tbl_obs`.`fields` AS `fields`,`tbl_obs`.`part_id` AS `part_id`,`tbl_obs`.`serial_num` AS `serial_num`,`tbl_obs`.`revisions` AS `revisions`,`tbl_obs`.`title` AS `title`,`tbl_obs`.`title_trans` AS `title_trans`,`tbl_obs`.`part_trans` AS `part_trans`,`tbl_obs`.`observation_target` AS `observation_target`,`tbl_obs`.`final_ver` AS `final_ver`,`tbl_obs`.`package_exclude` AS `package_exclude`,`tbl_obs`.`val_comp` AS `val_comp`,`tbl_obs`.`general_jdc` AS `general_jdc`,`tbl_obs`.`trans_range` AS `trans_range`,`tbl_obs`.`trans_deadline` AS `trans_deadline`,`tbl_obs`.`trans_lang` AS `trans_lang`,`tbl_obs`.`trans_state_req` AS `trans_state_req`,`tbl_obs`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_obs`.`trans_req_date` AS `trans_req_date`,`tbl_obs`.`translating_state` AS `translating_state`,`tbl_obs`.`translating_state_id` AS `translating_state_id`,`tbl_obs`.`translated_state` AS `translated_state`,`tbl_obs`.`translated_state_id` AS `translated_state_id`,`tbl_obs`.`approval_state_pr` AS `approval_state_pr`,`tbl_obs`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_obs`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_obs`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_obs`.`approval_state1` AS `approval_state1`,`tbl_obs`.`approval_state1_id` AS `approval_state1_id`,`tbl_obs`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_obs`.`approval_state2` AS `approval_state2`,`tbl_obs`.`approval_state2_id` AS `approval_state2_id`,`tbl_obs`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_obs`.`approval_state3` AS `approval_state3`,`tbl_obs`.`approval_state3_id` AS `approval_state3_id`,`tbl_obs`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_obs`.`approval_state_tl` AS `approval_state_tl`,`tbl_obs`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_obs`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_obs`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_obs`.`english_edition` AS `english_edition`,`tbl_obs`.`hold` AS `hold`,`tbl_obs`.`newest_flag` AS `newest_flag`,`tbl_obs`.`delete_flag` AS `delete_flag`,`tbl_obs`.`editor` AS `editor`,`tbl_obs`.`last_update` AS `last_update`,`tbl_obs`.`last_user` AS `last_user`,concat(`tbl_obs`.`plant_name`,'-',`tbl_obs`.`kinds`,'-',`tbl_obs`.`fields`,'-',`tbl_obs`.`part_id`,'-',convert(lpad(`tbl_obs`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_obs`.`revisions`,if((`tbl_obs`.`english_edition` = 'E'),'-E','')) AS `new_num`,concat(`tbl_obs`.`kinds`,'-',`tbl_obs`.`fields`,'-',`tbl_obs`.`part_id`,'-',convert(lpad(`tbl_obs`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_obs`.`revisions`,if((`tbl_obs`.`english_edition` = 'E'),'-E','')) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `tbl_obs`.`plant_name`) and (`tbl_field`.`fields` = `tbl_obs`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cansel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_cansel`,(select `tbl_obs`.`revisions` from `tbl_obs` `tblobs` where ((`tblobs`.`plant_name` = `tbl_obs`.`plant_name`) and (`tblobs`.`kinds` = `tbl_obs`.`kinds`) and (`tblobs`.`fields` = `tbl_obs`.`fields`) and (`tblobs`.`part_id` = `tbl_obs`.`part_id`) and (`tblobs`.`serial_num` = `tbl_obs`.`serial_num`) and (`tblobs`.`revisions` <= `tbl_obs`.`revisions`) and (`tblobs`.`delete_flag` = 0) and (`tblobs`.`translated_state` = 1)) order by concat(`tblobs`.`plant_name`,'-',`tblobs`.`kinds`,'-',`tblobs`.`fields`,'-',`tblobs`.`part_id`,'-',convert(lpad(`tblobs`.`serial_num`,2,'0') using utf8mb4),'-r',`tblobs`.`revisions`,if((`tblobs`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_rev`,(select `tbl_obs`.`translated_state` from `tbl_obs` `tblobs` where ((`tblobs`.`plant_name` = `tbl_obs`.`plant_name`) and (`tblobs`.`kinds` = `tbl_obs`.`kinds`) and (`tblobs`.`fields` = `tbl_obs`.`fields`) and (`tblobs`.`part_id` = `tbl_obs`.`part_id`) and (`tblobs`.`serial_num` = `tbl_obs`.`serial_num`) and (`tblobs`.`revisions` <= `tbl_obs`.`revisions`) and (`tblobs`.`delete_flag` = 0) and (`tblobs`.`translated_state` = 1)) order by concat(`tblobs`.`plant_name`,'-',`tblobs`.`kinds`,'-',`tblobs`.`fields`,'-',`tblobs`.`part_id`,'-',convert(lpad(`tblobs`.`serial_num`,2,'0') using utf8mb4),'-r',`tblobs`.`revisions`,if((`tblobs`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_status` from `tbl_obs` where ((`tbl_obs`.`newest_flag` = -(1)) and (`tbl_obs`.`delete_flag` = 0) and (`tbl_obs`.`hold` = -(1))) order by concat(`tbl_obs`.`plant_name`,'-',`tbl_obs`.`kinds`,'-',`tbl_obs`.`fields`,'-',`tbl_obs`.`part_id`,'-',convert(lpad(`tbl_obs`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_obs`.`revisions`,if((`tbl_obs`.`english_edition` = 'E'),'-E','')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `tbl_afi_extend`
--

/*!50001 DROP VIEW IF EXISTS `tbl_afi_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tbl_afi_extend` AS select `tbl_afi`.`id` AS `id`,`tbl_afi`.`plant_name` AS `plant_name`,`tbl_afi`.`years` AS `years`,`tbl_afi`.`kinds` AS `kinds`,`tbl_afi`.`fields` AS `fields`,`tbl_afi`.`part_id` AS `part_id`,`tbl_afi`.`serial_num` AS `serial_num`,`tbl_afi`.`revisions` AS `revisions`,`tbl_afi`.`achievement_goal` AS `achievement_goal`,`tbl_afi`.`title` AS `title`,`tbl_afi`.`title_trans` AS `title_trans`,`tbl_afi`.`part_trans` AS `part_trans`,`tbl_afi`.`observation_target` AS `observation_target`,`tbl_afi`.`final_ver` AS `final_ver`,`tbl_afi`.`package_exclude` AS `package_exclude`,`tbl_afi`.`val_comp` AS `val_comp`,`tbl_afi`.`general_jdc` AS `general_jdc`,`tbl_afi`.`connect_num` AS `connect_num`,`tbl_afi`.`connect_memo` AS `connect_memo`,`tbl_afi`.`connect_memo_trans` AS `connect_memo_trans`,`tbl_afi`.`connect_memo_parttrans` AS `connect_memo_parttrans`,`tbl_afi`.`trans_range` AS `trans_range`,`tbl_afi`.`trans_deadline` AS `trans_deadline`,`tbl_afi`.`trans_lang` AS `trans_lang`,`tbl_afi`.`trans_state_req` AS `trans_state_req`,`tbl_afi`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_afi`.`trans_req_date` AS `trans_req_date`,`tbl_afi`.`translating_state` AS `translating_state`,`tbl_afi`.`translating_state_id` AS `translating_state_id`,`tbl_afi`.`translated_state` AS `translated_state`,`tbl_afi`.`translated_state_id` AS `translated_state_id`,`tbl_afi`.`approval_state_pr` AS `approval_state_pr`,`tbl_afi`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_afi`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_afi`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_afi`.`approval_state1` AS `approval_state1`,`tbl_afi`.`approval_state1_id` AS `approval_state1_id`,`tbl_afi`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_afi`.`approval_state2` AS `approval_state2`,`tbl_afi`.`approval_state2_id` AS `approval_state2_id`,`tbl_afi`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_afi`.`approval_state3` AS `approval_state3`,`tbl_afi`.`approval_state3_id` AS `approval_state3_id`,`tbl_afi`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_afi`.`approval_state_tl` AS `approval_state_tl`,`tbl_afi`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_afi`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_afi`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_afi`.`english_edition` AS `english_edition`,`tbl_afi`.`hold` AS `hold`,`tbl_afi`.`newest_flag` AS `newest_flag`,`tbl_afi`.`delete_flag` AS `delete_flag`,`tbl_afi`.`editor` AS `editor`,`tbl_afi`.`last_update` AS `last_update`,`tbl_afi`.`last_user` AS `last_user`,concat(`tbl_afi`.`plant_name`,'-',`tbl_afi`.`kinds`,'-',`tbl_afi`.`fields`,'-',`tbl_afi`.`part_id`,'-',convert(lpad(`tbl_afi`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_afi`.`revisions`,if((`tbl_afi`.`english_edition` = 'E'),'-E','')) AS `new_num`,concat(`tbl_afi`.`kinds`,'-',`tbl_afi`.`fields`,'-',`tbl_afi`.`part_id`,'-',convert(lpad(`tbl_afi`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_afi`.`revisions`,if((`tbl_afi`.`english_edition` = 'E'),'-E','')) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `tbl_afi`.`plant_name`) and (`tbl_field`.`fields` = `tbl_afi`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cansel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_cansel`,(select `tbl_afi`.`revisions` from `tbl_afi` `tblafi` where ((`tblafi`.`plant_name` = `tbl_afi`.`plant_name`) and (`tblafi`.`kinds` = `tbl_afi`.`kinds`) and (`tblafi`.`fields` = `tbl_afi`.`fields`) and (`tblafi`.`part_id` = `tbl_afi`.`part_id`) and (`tblafi`.`serial_num` = `tbl_afi`.`serial_num`) and (`tblafi`.`revisions` <= `tbl_afi`.`revisions`) and (`tblafi`.`delete_flag` = 0) and (`tblafi`.`translated_state` = 1)) order by concat(`tblafi`.`plant_name`,'-',`tblafi`.`kinds`,'-',`tblafi`.`fields`,'-',`tblafi`.`part_id`,'-',convert(lpad(`tbl_afi`.`serial_num`,2,'0') using utf8mb4),'-r',`tblafi`.`revisions`,if((`tblafi`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_rev`,(select `tbl_afi`.`translated_state` from `tbl_afi` `tblafi` where ((`tblafi`.`plant_name` = `tbl_afi`.`plant_name`) and (`tblafi`.`kinds` = `tbl_afi`.`kinds`) and (`tblafi`.`fields` = `tbl_afi`.`fields`) and (`tblafi`.`part_id` = `tbl_afi`.`part_id`) and (`tblafi`.`serial_num` = `tbl_afi`.`serial_num`) and (`tblafi`.`revisions` <= `tbl_afi`.`revisions`) and (`tblafi`.`delete_flag` = 0) and (`tblafi`.`translated_state` = 1)) order by concat(`tblafi`.`plant_name`,'-',`tblafi`.`kinds`,'-',`tblafi`.`fields`,'-',`tblafi`.`part_id`,'-',convert(lpad(`tbl_afi`.`serial_num`,2,'0') using utf8mb4),'-r',`tblafi`.`revisions`,if((`tblafi`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_status` from `tbl_afi` where ((`tbl_afi`.`newest_flag` = -(1)) and (`tbl_afi`.`delete_flag` = 0) and (`tbl_afi`.`hold` = -(1))) order by concat(`tbl_afi`.`plant_name`,'-',`tbl_afi`.`kinds`,'-',`tbl_afi`.`fields`,'-',`tbl_afi`.`part_id`,'-',convert(lpad(`tbl_afi`.`serial_num`,2,'0') using utf8mb4),'-r',`tbl_afi`.`revisions`,if((`tbl_afi`.`english_edition` = 'E'),'-E','')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `tbl_bp_extend`
--

/*!50001 DROP VIEW IF EXISTS `tbl_bp_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tbl_bp_extend` AS select `tbl_bp`.`id` AS `id`,`tbl_bp`.`plant_name` AS `plant_name`,`tbl_bp`.`years` AS `years`,`tbl_bp`.`kinds` AS `kinds`,`tbl_bp`.`fields` AS `fields`,`tbl_bp`.`part_id` AS `part_id`,`tbl_bp`.`branch_num` AS `branch_num`,`tbl_bp`.`revisions` AS `revisions`,`tbl_bp`.`achievement_goal` AS `achievement_goal`,`tbl_bp`.`final_ver` AS `final_ver`,`tbl_bp`.`package_exclude` AS `package_exclude`,`tbl_bp`.`val_comp` AS `val_comp`,`tbl_bp`.`title` AS `title`,`tbl_bp`.`title_trans` AS `title_trans`,`tbl_bp`.`title_part_trans` AS `title_part_trans`,`tbl_bp`.`explan_text` AS `explan_text`,`tbl_bp`.`explan_text_trans` AS `explan_text_trans`,`tbl_bp`.`part_trans` AS `part_trans`,`tbl_bp`.`part_trans_explan` AS `part_trans_explan`,`tbl_bp`.`explan_text_translation` AS `explan_text_translation`,`tbl_bp`.`explan_text_jdc` AS `explan_text_jdc`,`tbl_bp`.`general_jdc` AS `general_jdc`,`tbl_bp`.`connect_num` AS `connect_num`,`tbl_bp`.`connect_memo` AS `connect_memo`,`tbl_bp`.`connect_memo_trans` AS `connect_memo_trans`,`tbl_bp`.`connect_memo_parttrans` AS `connect_memo_parttrans`,`tbl_bp`.`trans_range` AS `trans_range`,`tbl_bp`.`trans_deadline` AS `trans_deadline`,`tbl_bp`.`trans_lang` AS `trans_lang`,`tbl_bp`.`trans_state_req` AS `trans_state_req`,`tbl_bp`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_bp`.`trans_req_date` AS `trans_req_date`,`tbl_bp`.`translating_state` AS `translating_state`,`tbl_bp`.`translating_state_id` AS `translating_state_id`,`tbl_bp`.`translated_state` AS `translated_state`,`tbl_bp`.`translated_state_id` AS `translated_state_id`,`tbl_bp`.`approval_state_pr` AS `approval_state_pr`,`tbl_bp`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_bp`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_bp`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_bp`.`approval_state1` AS `approval_state1`,`tbl_bp`.`approval_state1_id` AS `approval_state1_id`,`tbl_bp`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_bp`.`approval_state2` AS `approval_state2`,`tbl_bp`.`approval_state2_id` AS `approval_state2_id`,`tbl_bp`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_bp`.`approval_state3` AS `approval_state3`,`tbl_bp`.`approval_state3_id` AS `approval_state3_id`,`tbl_bp`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_bp`.`approval_state_tl` AS `approval_state_tl`,`tbl_bp`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_bp`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_bp`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_bp`.`english_edition` AS `english_edition`,`tbl_bp`.`hold` AS `hold`,`tbl_bp`.`newest_flag` AS `newest_flag`,`tbl_bp`.`delete_flag` AS `delete_flag`,`tbl_bp`.`editor` AS `editor`,`tbl_bp`.`last_update` AS `last_update`,`tbl_bp`.`last_user` AS `last_user`,concat(`tbl_bp`.`plant_name`,'-',`tbl_bp`.`kinds`,'-',`tbl_bp`.`fields`,'-',`tbl_bp`.`part_id`,'-',convert(lpad(`tbl_bp`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_bp`.`revisions`,if((`tbl_bp`.`english_edition` = 'E'),'-E','')) AS `new_num`,concat(`tbl_bp`.`kinds`,'-',`tbl_bp`.`fields`,'-',`tbl_bp`.`part_id`,'-',convert(lpad(`tbl_bp`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_bp`.`revisions`,if((`tbl_bp`.`english_edition` = 'E'),'-E','')) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `tbl_bp`.`plant_name`) and (`tbl_field`.`fields` = `tbl_bp`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cansel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_cansel`,(select `tbl_bp`.`revisions` from `tbl_bp` `tblbp` where ((`tblbp`.`plant_name` = `tbl_bp`.`plant_name`) and (`tblbp`.`kinds` = `tbl_bp`.`kinds`) and (`tblbp`.`fields` = `tbl_bp`.`fields`) and (`tblbp`.`part_id` = `tbl_bp`.`part_id`) and (`tblbp`.`branch_num` = `tbl_bp`.`branch_num`) and (`tblbp`.`revisions` <= `tbl_bp`.`revisions`) and (`tblbp`.`delete_flag` = 0) and (`tblbp`.`translated_state` = 1)) order by concat(`tblbp`.`plant_name`,'-',`tblbp`.`kinds`,'-',`tblbp`.`fields`,'-',`tblbp`.`part_id`,'-',convert(lpad(`tbl_bp`.`branch_num`,2,'0') using utf8mb4),'-r',`tblbp`.`revisions`,if((`tblbp`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_rev`,(select `tbl_bp`.`translated_state` from `tbl_bp` `tblbp` where ((`tblbp`.`plant_name` = `tbl_bp`.`plant_name`) and (`tblbp`.`kinds` = `tbl_bp`.`kinds`) and (`tblbp`.`fields` = `tbl_bp`.`fields`) and (`tblbp`.`part_id` = `tbl_bp`.`part_id`) and (`tblbp`.`branch_num` = `tbl_bp`.`branch_num`) and (`tblbp`.`revisions` <= `tbl_bp`.`revisions`) and (`tblbp`.`delete_flag` = 0) and (`tblbp`.`translated_state` = 1)) order by concat(`tblbp`.`plant_name`,'-',`tblbp`.`kinds`,'-',`tblbp`.`fields`,'-',`tblbp`.`part_id`,'-',convert(lpad(`tbl_bp`.`branch_num`,2,'0') using utf8mb4),'-r',`tblbp`.`revisions`,if((`tblbp`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_status` from `tbl_bp` where ((`tbl_bp`.`newest_flag` = -(1)) and (`tbl_bp`.`delete_flag` = 0) and (`tbl_bp`.`hold` = -(1))) order by concat(`tbl_bp`.`plant_name`,'-',`tbl_bp`.`kinds`,'-',`tbl_bp`.`fields`,'-',`tbl_bp`.`part_id`,'-',convert(lpad(`tbl_bp`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_bp`.`revisions`,if((`tbl_bp`.`english_edition` = 'E'),'-E','')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `tbl_str_extend`
--

/*!50001 DROP VIEW IF EXISTS `tbl_str_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tbl_str_extend` AS select `tbl_str`.`id` AS `id`,`tbl_str`.`plant_name` AS `plant_name`,`tbl_str`.`years` AS `years`,`tbl_str`.`kinds` AS `kinds`,`tbl_str`.`fields` AS `fields`,`tbl_str`.`part_id` AS `part_id`,`tbl_str`.`branch_num` AS `branch_num`,`tbl_str`.`revisions` AS `revisions`,`tbl_str`.`achievement_goal` AS `achievement_goal`,`tbl_str`.`title` AS `title`,`tbl_str`.`title_trans` AS `title_trans`,`tbl_str`.`part_trans` AS `part_trans`,`tbl_str`.`val_comp` AS `val_comp`,`tbl_str`.`general_jdc` AS `general_jdc`,`tbl_str`.`final_ver` AS `final_ver`,`tbl_str`.`trans_range` AS `trans_range`,`tbl_str`.`trans_deadline` AS `trans_deadline`,`tbl_str`.`trans_lang` AS `trans_lang`,`tbl_str`.`trans_state_req` AS `trans_state_req`,`tbl_str`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_str`.`trans_req_date` AS `trans_req_date`,`tbl_str`.`translating_state` AS `translating_state`,`tbl_str`.`translating_state_id` AS `translating_state_id`,`tbl_str`.`translated_state` AS `translated_state`,`tbl_str`.`translated_state_id` AS `translated_state_id`,`tbl_str`.`approval_state_pr` AS `approval_state_pr`,`tbl_str`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_str`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_str`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_str`.`approval_state1` AS `approval_state1`,`tbl_str`.`approval_state1_id` AS `approval_state1_id`,`tbl_str`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_str`.`approval_state2` AS `approval_state2`,`tbl_str`.`approval_state2_id` AS `approval_state2_id`,`tbl_str`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_str`.`approval_state3` AS `approval_state3`,`tbl_str`.`approval_state3_id` AS `approval_state3_id`,`tbl_str`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_str`.`approval_state_tl` AS `approval_state_tl`,`tbl_str`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_str`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_str`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_str`.`english_edition` AS `english_edition`,`tbl_str`.`hold` AS `hold`,`tbl_str`.`newest_flag` AS `newest_flag`,`tbl_str`.`delete_flag` AS `delete_flag`,`tbl_str`.`editor` AS `editor`,`tbl_str`.`last_update` AS `last_update`,`tbl_str`.`last_user` AS `last_user`,concat(`tbl_str`.`plant_name`,'-',`tbl_str`.`kinds`,'-',`tbl_str`.`fields`,'-',`tbl_str`.`part_id`,'-',convert(lpad(`tbl_str`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_str`.`revisions`,if((`tbl_str`.`english_edition` = 'E'),'-E','')) AS `new_num`,concat(`tbl_str`.`kinds`,'-',`tbl_str`.`fields`,'-',`tbl_str`.`part_id`,'-',convert(lpad(`tbl_str`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_str`.`revisions`,if((`tbl_str`.`english_edition` = 'E'),'-E','')) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `tbl_str`.`plant_name`) and (`tbl_field`.`fields` = `tbl_str`.`fields`)) limit 1) AS `FieldOrder`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cansel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_cansel`,(select `tbl_str`.`revisions` from `tbl_str` `tblstr` where ((`tblstr`.`plant_name` = `tbl_str`.`plant_name`) and (`tblstr`.`kinds` = `tbl_str`.`kinds`) and (`tblstr`.`fields` = `tbl_str`.`fields`) and (`tblstr`.`part_id` = `tbl_str`.`part_id`) and (`tblstr`.`branch_num` = `tbl_str`.`branch_num`) and (`tblstr`.`revisions` <= `tbl_str`.`revisions`) and (`tblstr`.`delete_flag` = 0) and (`tblstr`.`translated_state` = 1)) order by concat(`tblstr`.`plant_name`,'-',`tblstr`.`kinds`,'-',`tblstr`.`fields`,'-',`tblstr`.`part_id`,'-',convert(lpad(`tbl_str`.`branch_num`,2,'0') using utf8mb4),'-r',`tblstr`.`revisions`,if((`tblstr`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_rev`,(select `tbl_str`.`translated_state` from `tbl_str` `tblstr` where ((`tblstr`.`plant_name` = `tbl_str`.`plant_name`) and (`tblstr`.`kinds` = `tbl_str`.`kinds`) and (`tblstr`.`fields` = `tbl_str`.`fields`) and (`tblstr`.`part_id` = `tbl_str`.`part_id`) and (`tblstr`.`branch_num` = `tbl_str`.`branch_num`) and (`tblstr`.`revisions` <= `tbl_str`.`revisions`) and (`tblstr`.`delete_flag` = 0) and (`tblstr`.`translated_state` = 1)) order by concat(`tblstr`.`plant_name`,'-',`tblstr`.`kinds`,'-',`tblstr`.`fields`,'-',`tblstr`.`part_id`,'-',convert(lpad(`tbl_str`.`branch_num`,2,'0') using utf8mb4),'-r',`tblstr`.`revisions`,if((`tblstr`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_status` from `tbl_str` where ((`tbl_str`.`newest_flag` = -(1)) and (`tbl_str`.`delete_flag` = 0) and (`tbl_str`.`hold` = -(1))) order by concat(`tbl_str`.`plant_name`,'-',`tbl_str`.`kinds`,'-',`tbl_str`.`fields`,'-',`tbl_str`.`part_id`,'-',convert(lpad(`tbl_str`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_str`.`revisions`,if((`tbl_str`.`english_edition` = 'E'),'-E','')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `tbl_soi_extend`
--

/*!50001 DROP VIEW IF EXISTS `tbl_soi_extend`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `tbl_soi_extend` AS select `tbl_soi`.`id` AS `id`,`tbl_soi`.`plant_name` AS `plant_name`,`tbl_soi`.`years` AS `years`,`tbl_soi`.`kinds` AS `kinds`,`tbl_soi`.`fields` AS `fields`,`tbl_soi`.`part_id` AS `part_id`,`tbl_soi`.`branch_num` AS `branch_num`,`tbl_soi`.`revisions` AS `revisions`,`tbl_soi`.`achievement_goal` AS `achievement_goal`,`tbl_soi`.`title` AS `title`,`tbl_soi`.`title_trans` AS `title_trans`,`tbl_soi`.`part_trans` AS `part_trans`,`tbl_soi`.`final_ver` AS `final_ver`,`tbl_soi`.`package_exclude` AS `package_exclude`,`tbl_soi`.`follow_up_num` AS `follow_up_num`,`tbl_soi`.`general_jdc` AS `general_jdc`,`tbl_soi`.`trans_range` AS `trans_range`,`tbl_soi`.`trans_deadline` AS `trans_deadline`,`tbl_soi`.`trans_lang` AS `trans_lang`,`tbl_soi`.`trans_state_req` AS `trans_state_req`,`tbl_soi`.`trans_state_req_id` AS `trans_state_req_id`,`tbl_soi`.`trans_req_date` AS `trans_req_date`,`tbl_soi`.`translating_state` AS `translating_state`,`tbl_soi`.`translating_state_id` AS `translating_state_id`,`tbl_soi`.`translated_state` AS `translated_state`,`tbl_soi`.`translated_state_id` AS `translated_state_id`,`tbl_soi`.`approval_state_pr` AS `approval_state_pr`,`tbl_soi`.`approval_state_pr_id` AS `approval_state_pr_id`,`tbl_soi`.`approval_state_pr_rev` AS `approval_state_pr_rev`,`tbl_soi`.`approval_state_pr_state` AS `approval_state_pr_state`,`tbl_soi`.`approval_state1` AS `approval_state1`,`tbl_soi`.`approval_state1_id` AS `approval_state1_id`,`tbl_soi`.`approval_state1_rev` AS `approval_state1_rev`,`tbl_soi`.`approval_state2` AS `approval_state2`,`tbl_soi`.`approval_state2_id` AS `approval_state2_id`,`tbl_soi`.`approval_state2_rev` AS `approval_state2_rev`,`tbl_soi`.`approval_state3` AS `approval_state3`,`tbl_soi`.`approval_state3_id` AS `approval_state3_id`,`tbl_soi`.`approval_state3_rev` AS `approval_state3_rev`,`tbl_soi`.`approval_state_tl` AS `approval_state_tl`,`tbl_soi`.`approval_state_tl_id` AS `approval_state_tl_id`,`tbl_soi`.`approval_state_tl_rev` AS `approval_state_tl_rev`,`tbl_soi`.`approval_state_tl_state` AS `approval_state_tl_state`,`tbl_soi`.`english_edition` AS `english_edition`,`tbl_soi`.`hold` AS `hold`,`tbl_soi`.`newest_flag` AS `newest_flag`,`tbl_soi`.`delete_flag` AS `delete_flag`,`tbl_soi`.`editor` AS `editor`,`tbl_soi`.`last_update` AS `last_update`,`tbl_soi`.`last_user` AS `last_user`,concat(`tbl_soi`.`plant_name`,'-',`tbl_soi`.`kinds`,'-',`tbl_soi`.`fields`,'-',`tbl_soi`.`part_id`,'-',convert(lpad(`tbl_soi`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_soi`.`revisions`,if((`tbl_soi`.`english_edition` = 'E'),'-E','')) AS `new_num`,concat(`tbl_soi`.`kinds`,'-',`tbl_soi`.`fields`,'-',`tbl_soi`.`part_id`,'-',convert(lpad(`tbl_soi`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_soi`.`revisions`,if((`tbl_soi`.`english_edition` = 'E'),'-E','')) AS `l_num`,(select `tbl_field`.`output_order` from `tbl_field` where ((`tbl_field`.`plant_name` = `tbl_soi`.`plant_name`) and (`tbl_field`.`fields` = `tbl_soi`.`fields`)) limit 1) AS `field_order`,(select `tbl_translate`.`trans_situation` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_situation`,(select `tbl_translate`.`cansel_status` from `tbl_translate` where (`tbl_translate`.`offer_num` = (select max(`tbl_translate`.`offer_num`) from `tbl_translate` where (concat(`tbl_translate`.`plant_name`,'-',`tbl_translate`.`num`) = `new_num`))) limit 1) AS `trans_cansel`,(select `tbl_soi`.`revisions` from `tbl_soi` `tblsoi` where ((`tblsoi`.`plant_name` = `tbl_soi`.`plant_name`) and (`tblsoi`.`kinds` = `tbl_soi`.`kinds`) and (`tblsoi`.`fields` = `tbl_soi`.`fields`) and (`tblsoi`.`part_id` = `tbl_soi`.`part_id`) and (`tblsoi`.`branch_num` = `tbl_soi`.`branch_num`) and (`tblsoi`.`revisions` <= `tbl_soi`.`revisions`) and (`tblsoi`.`delete_flag` = 0) and (`tblsoi`.`translated_state` = 1)) order by concat(`tblsoi`.`plant_name`,'-',`tblsoi`.`kinds`,'-',`tblsoi`.`fields`,'-',`tblsoi`.`part_id`,'-',convert(lpad(`tbl_soi`.`branch_num`,2,'0') using utf8mb4),'-r',`tblsoi`.`revisions`,if((`tblsoi`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_rev`,(select `tbl_soi`.`translated_state` from `tbl_soi` `tblsoi` where ((`tblsoi`.`plant_name` = `tbl_soi`.`plant_name`) and (`tblsoi`.`kinds` = `tbl_soi`.`kinds`) and (`tblsoi`.`fields` = `tbl_soi`.`fields`) and (`tblsoi`.`part_id` = `tbl_soi`.`part_id`) and (`tblsoi`.`branch_num` = `tbl_soi`.`branch_num`) and (`tblsoi`.`revisions` <= `tbl_soi`.`revisions`) and (`tblsoi`.`delete_flag` = 0) and (`tblsoi`.`translated_state` = 1)) order by concat(`tblsoi`.`plant_name`,'-',`tblsoi`.`kinds`,'-',`tblsoi`.`fields`,'-',`tblsoi`.`part_id`,'-',convert(lpad(`tbl_soi`.`branch_num`,2,'0') using utf8mb4),'-r',`tblsoi`.`revisions`,if((`tblsoi`.`english_edition` = 'E'),'-E','')) desc limit 1) AS `past_translated_status` from `tbl_soi` where ((`tbl_soi`.`newest_flag` = -(1)) and (`tbl_soi`.`delete_flag` = 0) and (`tbl_soi`.`hold` = -(1))) order by concat(`tbl_soi`.`plant_name`,'-',`tbl_soi`.`kinds`,'-',`tbl_soi`.`fields`,'-',`tbl_soi`.`part_id`,'-',convert(lpad(`tbl_soi`.`branch_num`,2,'0') using utf8mb4),'-r',`tbl_soi`.`revisions`,if((`tbl_soi`.`english_edition` = 'E'),'-E','')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Dumping routines for database 'fnd'
--
/*!50003 DROP FUNCTION IF EXISTS `Fun_Get_Dm_DivisionF` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fun_Get_Dm_DivisionF`(in_PlantName varchar(50)) RETURNS varchar(1) CHARSET utf8mb4
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
/*!50003 DROP FUNCTION IF EXISTS `Fun_Get_Max_No` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fun_Get_Max_No`(plantName varchar(50), field varchar(30), dmDivision varchar(1), partId varchar(3)) RETURNS int
    DETERMINISTIC
BEGIN
	DECLARE maxId INT;
		IF(dmDivision = "F") THEN
			SET field := "FU";
		END IF;

		SET maxId = (SELECT COALESCE(MAX(serial_num),0) + 1 AS serial_num 
					FROM tbl_obs
					WHERE plant_name =  plantName
					AND
					   kinds = 'OBS'
					AND
					   fields = field
					AND
					   part_id = partId);
	RETURN maxId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `Fun_Get_Max_Offer_Num_Tbl_Tran` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fun_Get_Max_Offer_Num_Tbl_Tran`(plantName varchar(50)) RETURNS int
    DETERMINISTIC
BEGIN
	DECLARE maxId INT;
	SET maxId = (SELECT COALESCE(MAX(offer_num),0) + 1 AS offer_num 
				 FROM tbl_translate
				 WHERE plant_name =  plantName
				);
	RETURN maxId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `Fun_Jap_Eng_Con_App_Status` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Fun_Jap_Eng_Con_App_Status`(pLang varchar(1), pComment varchar(5000)) RETURNS varchar(5000) CHARSET utf8mb4
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
													   in_Lang VARCHAR(1))
BEGIN
	DECLARE intMaxNo INT;
    DECLARE strNum VARCHAR(50);
    DECLARE strNumForPhoto VARCHAR(50);
    DECLARE sqlStr TEXT;
    -- SET intMaxNo = (select Fun_Get_Max_No('kawa','FU','F',123));
   SET intMaxNo = (select Fun_Get_Max_No(in_PlantName, in_Area, in_DmDivision, in_PartId));
    
    SET sqlStr := "INSERT INTO tbl_obs";
    SET sqlStr := CONCAT(sqlStr , " (plant_name"); -- プラント名
    SET sqlStr := CONCAT(sqlStr , ", kinds"); -- 種類
    SET sqlStr := CONCAT(sqlStr , ", `fields`"); -- 分野
    SET sqlStr := CONCAT(sqlStr , ", part_id"); -- 個別ID
    SET sqlStr := CONCAT(sqlStr , ", serial_num"); -- 通番
    SET sqlStr := CONCAT(sqlStr , ", revisions"); -- リビジョン
    SET sqlStr := CONCAT(sqlStr , ", english_edition"); -- 英語版
    SET sqlStr := CONCAT(sqlStr , ", editor"); -- 編集者
    SET sqlStr := CONCAT(sqlStr , ", last_user"); -- 最終更新者
    SET sqlStr := CONCAT(sqlStr , ") VALUES");
    SET sqlStr := CONCAT(sqlStr , " (? "); -- plantName
    SET sqlStr := CONCAT(sqlStr , ", '" , "OBS" , "'");-- 種類
    
    IF in_DmDivision = "F" THEN
        SET sqlStr := CONCAT(sqlStr , ", '" , "FU" , "'");-- 分野
    Else
        SET sqlStr := CONCAT(sqlStr , ",? "); -- area -- 分野
    End IF;
    
    SET sqlStr := CONCAT(sqlStr , ", ? "); -- partId -- 個別ID
    SET sqlStr := CONCAT(sqlStr , ", ? "); -- intMaxNo -- 通番
    SET sqlStr := CONCAT(sqlStr , ", '" , 0 , "'");-- リビジョン revisions
    SET sqlStr := CONCAT(sqlStr , ", ? "); -- lang-- 英語版
    SET sqlStr := CONCAT(sqlStr ,", ? "); -- partId (editor)
    SET sqlStr := CONCAT(sqlStr , ", ? "); -- partId (last_user)
    SET sqlStr := CONCAT(sqlStr , ");");
    
    set @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @plantName_val = in_PlantName;
	SET @area_val = in_Area;
    SET @partId_val = in_PartId;
    SET @intMaxNo_val = intMaxNo;
    SET @lang_val = in_Lang;
    SET @editor_val = in_PartId;
    SET @last_user_val = in_PartId;
    IF in_DmDivision = "F" THEN
		EXECUTE dynamic_statement USING @plantName_val, @partId_val, @intMaxNo_val, @lang_val, @editor_val, @last_user_val;
    ELSE
		EXECUTE dynamic_statement USING @plantName_val, @area_val , @partId_val, @intMaxNo_val, @lang_val, @editor_val, @last_user_val;
    END IF;
    DEALLOCATE PREPARE dynamic_statement;
    set sqlStr := '';
    
    -- str番号 = g_strPlantName & "-" & "OBS" & "-" & IIF(Nz(g_strDM区分) = "F", "FU", g_strEvalArea) & "-" & g_strUserInitials & "-" & Format(intMaxNo, "00") & "-" & "r0" & IIF(g_strLang = "E", "-E", "")
    SET strNum := CONCAT( in_PlantName , "-" , "OBS" , "-" , IF(in_DmDivision = "F", "FU", in_Area) , "-" , in_PartId , "-" , LPAD(intMaxNo,2, "0") , "-" , "r0" , IF(in_Lang = "E", "-E", ""));
    
    SET sqlStr := CONCAT(sqlStr, " INSERT INTO tbl_obs_scope ( `num` ) VALUES ( ? ); "); -- strNum
    
    set @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @num_val = strNum;
    EXECUTE dynamic_statement USING @num_val;
    DEALLOCATE PREPARE dynamic_statement;
    
    set sqlStr := '';    
    SET sqlStr := CONCAT(sqlStr, " INSERT INTO tbl_obs_fact (num) VALUES ( ? ); ");
    
    set @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @num_val = strNum;
    EXECUTE dynamic_statement USING @num_val;
    DEALLOCATE PREPARE dynamic_statement;
    set sqlStr := '';
    
    SET sqlStr := CONCAT(sqlStr, " INSERT INTO tbl_obs_conclusion (num) VALUES ( ? ); ");
    set @sqlexc :=sqlStr;
    PREPARE dynamic_statement FROM @sqlexc;
    SET @num_val = strNum;
    EXECUTE dynamic_statement USING @num_val;
    DEALLOCATE PREPARE dynamic_statement;
    set sqlStr := '';
    
    -- str番号_写真用 = g_strPlantName & "-" & "OBS" & "-" & IIF(Nz(g_strDM区分) = "F", "FU", g_strEvalArea) & "-" & g_strUserInitials & "-" & Format(intMaxNo, "00")
    SET strNumForPhoto := CONCAT(in_PlantName , "-" , "OBS" , "-" , IF(in_DmDivision = "F", "FU", in_Area) , "-" , in_PartId , "-" , LPAD(intMaxNo,2, "0")); 
    SET sqlStr := CONCAT(sqlStr, " INSERT INTO tbl_obs_attach (num) VALUES ( ? );"); -- strNumForPhoto
   
    -- select sqlStr;
   set @sqlexc :=sqlStr;
   PREPARE dynamic_statement FROM @sqlexc;
   SET @num_val = strNumForPhoto;
   EXECUTE dynamic_statement USING @num_val;
   DEALLOCATE PREPARE dynamic_statement;

   set sqlStr := '';
   SET sqlStr := CONCAT(sqlStr,"SELECT * FROM tbl_obs ORDER BY id DESC LIMIT 1;");
   set @sqlexc :=sqlStr;
   PREPARE dynamic_statement FROM @sqlexc;
   EXECUTE dynamic_statement;
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
	declare sqlStr text;
   
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
        SET strSQL := Concat(strSQL," WHERE (display_order >= 1 AND display_order <= 11)");
        SET strSQL := Concat(strSQL," AND dm_div = Fun_Get_Dm_DivisionF(?) "); -- in_PlantName
        SET strSQL := Concat(strSQL," AND plant_name = ?"); -- in_PlantName
        SET strSQL := Concat(strSQL," ORDER BY display_order LIMIT 11;");
     ELSE
		SET strSQL :="SELECT * FROM tbl_Field";
        SET strSQL := Concat(strSQL,"WHERE (display_order >= 1 AND display_order <= 11)");
        SET strSQL := Concat(strSQL," AND dm_div = ?"); -- in_DmDivision
        SET strSQL := Concat(strSQL," AND plant_name = ?"); -- in_PlantName
        SET strSQL := Concat(strSQL," ORDER BY display_order LIMIT 11;");
	 END IF;
   -- select strSQL;
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
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Data_Null_ForPrint` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Data_Null_ForPrint`()
BEGIN
		Select null as id,null as plant_name,null as years,
			   null as kinds,null as fields,null as part_id,
			   null as serial_num,null as revisions,null as title,
			   null as title_trans,null as part_trans,null as observation_target,
			   null as final_ver,null as package_exclude ,null as val_comp,null as general_jdc,
			   null as trans_range,null as trans_deadline,null as trans_lang,null as trans_state_req,
			   null as trans_state_req_id,null as trans_req_date,null as translating_state,null as translating_state_id,
			   null as translated_state,null as translated_state_id,null as approval_state_pr,null as approval_state_pr_id,
			   null as approval_state_pr_rev,null as approval_state_pr_state,null as approval_state1,null as approval_state1_id,
			   null as approval_state1_rev,null as approval_state2,null as approval_state2_id,null as approval_state2_rev,
			   null as approval_state3,null as approval_state3_id,null as approval_state3_rev,null as approval_state_tl,
			   null as approval_state_tl_id,null as approval_state_tl_rev,null as approval_state_tl_state,null as english_edition,
			   null as hold,null as newest_flag,null as delete_flag,null as editor,
			   null as last_update,null as last_user,null as Num,null as LNum,
			   null as FieldOrder,null as TransSituation,null as TransCansel,null as PastTranslatedRev,
			   null as PastTranslatedStatus,null as LTitle,null as ValStatus,null as LTransRange,
			   null as TlApprovalStatus;
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
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Field`(in_Lang varchar(1), in_PlantName varchar(50))
BEGIN
    declare strSQL text;
	set strSQL :=	"SELECT	
						tbl_field.display_order as DisplayOrder,
						tbl_field.fields,  ";-- 分野
						-- IIf(Nz(G_getLang(),"")='E',[tbl_Field]![fields_name_en][分野名（英語）],[tbl_Field]![fields_name][分野名]) AS [fields_name][分野名] 	
	set strSQL := CONCAT(strSQL, "If( ? ='E',tbl_field.fields_name_en, tbl_field.fields_name) AS FieldsName"); -- in_Lang
	set strSQL := CONCAT(strSQL, "	FROM	tbl_field " );
    set strSQL := CONCAT(strSQL, " WHERE	plant_name = ? "); -- in_PlantName
    set strSQL := CONCAT(strSQL," ORDER BY DisplayOrder;");-- 表示順
	
   -- select strSQL;
   set @sqlexc :=strSQL;
   PREPARE dynamic_statement FROM @sqlexc;
   SET @lang_val = in_Lang;
   SET @plantName_val= in_PlantName;
   EXECUTE dynamic_statement USING @lang_val, @plantName_val;
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
		DECLARE strSQL text;
         -- [イニシャル]
         -- 氏名
		SET strSQL := "SELECT	tbl_reviewmember.initial,
							tbl_reviewmember.name
					   FROM	tbl_reviewmember
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
	DECLARE strSQL text;
	SET strSQL := "SELECT tbl_Translate.offer_num ";
	SET strSQL := CONCAT(strSQL," FROM tbl_obs_extend");
    SET strSQL := CONCAT(strSQL," INNER JOIN tbl_Translate ON (tbl_obs_extend.plant_name = tbl_Translate.plant_name)");
	SET strSQL := CONCAT(strSQL,"AND (tbl_obs_extend.kinds = tbl_Translate.kinds)");
	SET strSQL := CONCAT(strSQL,"AND (tbl_obs_extend.fields = tbl_Translate.fields)");
	SET strSQL := CONCAT(strSQL,"AND (tbl_obs_extend.part_id = tbl_Translate.part_id)");
	SET strSQL := CONCAT(strSQL,"AND (tbl_obs_extend.serial_num = tbl_Translate.branch_num) ");
	SET strSQL := CONCAT(strSQL," WHERE tbl_obs_extend.plant_name = ?");
    SET strSQL := CONCAT(strSQL, " AND tbl_obs_extend.new_num = ?");
	SET strSQL := CONCAT(strSQL, " AND (tbl_obs_extend.approval_state_pr_rev = '' Or tbl_obs_extend.approval_state_pr_rev < tbl_obs_extend.past_translated_rev)");
    SET strSQL := CONCAT(strSQL, " AND tbl_obs_extend.past_translated_status = True");
	SET strSQL := CONCAT(strSQL," AND tbl_obs_extend.revisions > tbl_Translate.revisions");
	SET strSQL := CONCAT(strSQL," AND tbl_Translate.trans_situation = '取込済'");
	SET strSQL := CONCAT(strSQL," ORDER BY tbl_Translate.revisions, tbl_Translate.offer_num;");
    -- select strSQL;
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
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Obs_Extend_List_01` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Obs_Extend_List_01`(in_PlantName varchar(10), in_Area varchar(30), in_Initial varchar(50), in_Lang varchar(1), in_TL tinyint, in_TxtFreeWord varchar(50))
BEGIN
 -- G_cns区切文字_文字
    DECLARE G_cns_delimiter_character VARCHAR(1000);
    DECLARE strSQL text;
    
    SET G_cns_delimiter_character :="@@@";
	SET strSQL := "SELECT tbl_obs_extend.*";
    -- in_Lang: "ｵﾘｼﾞﾅﾙ版／Original";"0";"日本語版／Japanese";"1";"英語版／English";"2"
    CASE in_Lang
		WHEN '1' THEN
		   SET strSQL := CONCAT(strSQL , ", tbl_obs_extend.title AS `l_title`"); -- Lタイトル
		   SET strSQL := CONCAT(strSQL , ", IF(val_comp= True, '完了', '未完') AS `val_status`"); -- VAL状況
		   SET strSQL := CONCAT(strSQL , ", IF(trans_range = 0, 'なし', IF(trans_range = '1', '部分', IF(trans_range = '2', '全文', ''))) AS `l_trans_range`"); -- L翻訳範囲
		   SET strSQL := CONCAT(strSQL , ", IF(approval_state_pr=True,");
		   SET strSQL := CONCAT(strSQL , "  CONCAT( '【レビュワ】' , Fun_Jap_Eng_Con_App_Status('',approval_state_pr_state) ),");
		   SET strSQL := CONCAT(strSQL , "  CONCAT( IF(approval_state1 = True, '【審1】', '') ,  IF(approval_state2 = True, '【審2】', '') , IF(approval_state3 = True, '【審3】', '') , IF(approval_state_tl = True, '【TL】', '') , Fun_Jap_Eng_Con_App_Status('', approval_state_tl_state) )) AS `tl_approval_status`"); -- TL承認状況
		WHEN '2' THEN
		   SET strSQL := CONCAT(strSQL , ", IF(tbl_obs_extend.title_trans = '', 'making', tbl_obs_extend.title_trans) AS `l_title`");-- Lタイトル
		   SET strSQL := CONCAT(strSQL , ", IF(val_comp= True, 'Complete', 'Incomplete') AS `val_status`");-- VAL状況
		   SET strSQL := CONCAT(strSQL , ", IF(trans_range = 0, 'Nothing', IF(trans_range = 1, 'Partial', IF(trans_range = '2', 'Whole', ''))) AS `l_trans_range`");-- L翻訳範囲
		   SET strSQL := CONCAT(strSQL , ", IF(approval_state_pr=True,");
		   SET strSQL := CONCAT(strSQL , "  CONCAT( '【Reviewer】' , Fun_Jap_Eng_Con_App_Status('E',approval_state_pr_state)),");
		   SET strSQL := CONCAT(strSQL , "  CONCAT( IF(approval_state1 = True, '【As.1】', '') ,  IF(approval_state2 = True, '【As.2】', '') , IF(approval_state3 = True, '【As.3】', '') , IF(approval_state_tl = True, '【TL】','') , Fun_Jap_Eng_Con_App_Status('E', approval_state_tl_state) )) AS `tl_approval_status`");-- TL承認状況
     ELSE
		BEGIN
			  SET  strSQL := CONCAT(strSQL , ", IF(english_edition = 'E',");
			  SET  strSQL := CONCAT(strSQL , " tbl_obs_extend.title_trans" , ",");
			  SET  strSQL := CONCAT(strSQL , " tbl_obs_extend.title" , ") AS `l_title`");-- Lタイトル
			  SET  strSQL := CONCAT(strSQL , ", IF(english_edition = 'E',");
			  SET  strSQL := CONCAT(strSQL , " IF(val_comp= True, 'Complete', 'Incomplete')" , ",");
			  SET  strSQL := CONCAT(strSQL , " IF(val_comp= True, '完了', '未完')" , ") AS `val_status`"); -- VAL状況
			  SET  strSQL := CONCAT(strSQL , ", IF(english_edition = 'E',");
			  SET  strSQL := CONCAT(strSQL , " IF(trans_range = 0, 'Nothing', IF(trans_range = 1, 'Partial', IF(trans_range = '2', 'Whole', '')))" , ",");
			  SET  strSQL := CONCAT(strSQL , " IF(trans_range = 0, 'なし', IF(trans_range = '1', '部分', IF(trans_range = '2', '全文', '')))" , ") AS `l_trans_range`");-- L翻訳範囲
			  SET  strSQL := CONCAT(strSQL , ", IF(approval_state_pr=True,");
			  SET  strSQL := CONCAT(strSQL , "  CONCAT( IF(english_edition = 'E','【Reviewer】','【レビュワ】') , Fun_Jap_Eng_Con_App_Status(english_edition, approval_state_pr_state) ),");
			  SET  strSQL := CONCAT(strSQL , "  CONCAT( IF(approval_state1 = True, IF(english_edition = 'E','【As.1】','【審1】'),'') , IF(approval_state2=True, IF(english_edition = 'E','【As.2】','【審2】'),'') , ");
			  SET  strSQL := CONCAT(strSQL , "   IF(approval_state3 = True, IF(english_edition = 'E','【As.3】','【審3】'),'') , IF(approval_state_tl = True,'【TL】',''), Fun_Jap_Eng_Con_App_Status(english_edition, approval_state_tl_state) )) AS `tl_approval_status`");-- TL承認状況
	    END;
    END CASE;
    
    IF in_PlantName <> "" THEN
		SET strSQL := CONCAT(strSQL , " FROM tbl_obs_extend WHERE tbl_obs_extend.plant_name = ? "); -- 1 :in_PlantName
	ELSE
		SET strSQL := CONCAT(strSQL , " FROM tbl_obs_extend ");
	END IF;
    
    IF in_Initial <> "" OR in_Area <> "" OR in_TxtFreeWord <> "" OR in_TL = True THEN
		IF in_PlantName <> "" THEN
			SET strSQL := CONCAT(strSQL , " AND tbl_obs_extend.new_num IN (SELECT Distinct(tbl_obs_extend.new_num)");
        ELSE
			SET strSQL := CONCAT(strSQL , " WHERE tbl_obs_extend.new_num IN (SELECT Distinct(tbl_obs_extend.new_num)");
        END IF;
        
        SET strSQL := CONCAT(strSQL , " FROM tbl_obs_extend");
        SET strSQL := CONCAT(strSQL , " LEFT JOIN tbl_obs_scope ON tbl_obs_extend.new_num = tbl_obs_scope.num");
        SET strSQL := CONCAT(strSQL , " LEFT JOIN tbl_obs_conclusion ON tbl_obs_extend.new_num = tbl_obs_conclusion.num");
        SET strSQL := CONCAT(strSQL , " LEFT JOIN tbl_obs_fact ON tbl_obs_extend.new_num = tbl_obs_fact.num");

        IF in_Area <> "" THEN
            SET strSQL := CONCAT(strSQL , " WHERE tbl_obs_extend.fields = ? "); -- 2 in_Area
        END IF;
        
        IF in_Initial <> "" THEN
            IF in_Area <> "" THEN
                SET strSQL := CONCAT(strSQL , " AND");
            ELSE
                SET strSQL := CONCAT(strSQL , " WHERE");
            END IF;
            SET strSQL := CONCAT(strSQL , " tbl_obs_extend.part_id = ? "); -- 3 in_Initial
        END IF;
        
        IF in_TxtFreeWord <> "" THEN
            IF area <> "" OR in_Initial <> "" THEN
                SET strSQL := CONCAT(strSQL , " AND ");
            Else
                SET strSQL := CONCAT(strSQL , " WHERE ");
            END IF;
            -- in_Lang: "ｵﾘｼﾞﾅﾙ版／Original";"0";"日本語版／Japanese";"1";"英語版／English";"2"
            CASE in_Lang
				WHEN '1' THEN
					SET strSQL := CONCAT(strSQL , " (tbl_obs_extend.title Like '% ? %'"); -- 4. in_TxtFreeWord : title
					SET strSQL := CONCAT(strSQL , " OR tbl_obs_scope.scope Like '% ? %'"); -- 5. in_TxtFreeWord : scope
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_conclusion.conclusion, '" , G_cns_delimiter_character , "', '') Like '% ? %'"); -- 6. in_TxtFreeWord : conclusion
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_fact.fact, '" , G_cns_delimiter_character , "', '') Like '% ? %'" , ")"); -- 7. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 7. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 8. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 9. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 10. in_TxtFreeWord : fact
				WHEN '2' THEN
					SET strSQL := CONCAT(strSQL , " (tbl_obs_extend.title_trans Like '% ? %'"); -- 8. in_TxtFreeWord : title_trans
					SET strSQL := CONCAT(strSQL , " OR tbl_obs_scope.scope_trans Like '% ? %'"); -- 9. in_TxtFreeWord : scope_trans
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_conclusion.conclusion_trans, '" , G_cns_delimiter_character , "', '') Like '% ? %'"); -- 10. in_TxtFreeWord : conclusion_trans
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_fact.fact_trans, '" , G_cns_delimiter_character , "', '') Like '% ? %'" , ")"); -- 11. in_TxtFreeWord : fact_trans
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 7. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 8. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 9. in_TxtFreeWord : fact
                    SET strSQL := CONCAT(strSQL , " AND (? = null OR 1 = 1)"); -- 10. in_TxtFreeWord : fact
				ELSE
					SET strSQL := CONCAT(strSQL , " IF(english_edition = 'E',");
					SET strSQL := CONCAT(strSQL , " (tbl_obs_extend.title_trans Like '% ? %'"); -- 12. in_TxtFreeWord : title_trans
					SET strSQL := CONCAT(strSQL , " OR tbl_obs_scope.scope_trans Like '% ? %'"); -- 13. in_TxtFreeWord : scope_trans
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_conclusion.conclusion_trans, '" , G_cns_delimiter_character , "', '') Like '% ? %'"); -- 14. in_TxtFreeWord : conclusion_trans
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_fact.fact_trans, '" , G_cns_delimiter_character , "', '') Like '% ? %'" , "),"); -- 15. in_TxtFreeWord : fact_trans
					SET strSQL := CONCAT(strSQL , " (tbl_obs_extend.title Like '% ? %'"); -- 16. in_TxtFreeWord : title
					SET strSQL := CONCAT(strSQL , " OR tbl_obs_scope.scope Like '% ? %'"); -- 17. in_TxtFreeWord : scope
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_conclusion.conclusion, '" , G_cns_delimiter_character , "', '') Like '% ? %'"); -- 18. in_TxtFreeWord : conclusion
					SET strSQL := CONCAT(strSQL , " OR Replace(tbl_obs_fact.fact, '" , G_cns_delimiter_character , "', '') Like '% ? %'" , "))"); -- 19. in_TxtFreeWord : fact
				END CASE;
        END IF;

        IF in_TL = True THEN
            IF in_Area <> "" OR in_Initial <> "" OR in_TxtFreeWord <> "" THEN
                SET strSQL := CONCAT(strSQL , " AND ");
            Else
                SET strSQL := CONCAT(strSQL , " WHERE ");
            END IF;
            SET strSQL := CONCAT(strSQL , " tbl_obs_extend.approval_state_tl = True");
        END IF;

        SET strSQL := CONCAT(strSQL , ")");

        SET strSQL := CONCAT(strSQL , " ORDER BY tbl_obs_extend.field_order, tbl_obs_extend.new_num;");
	ELSE
		SET strSQL := CONCAT(strSQL , " ORDER BY tbl_obs_extend.field_order, tbl_obs_extend.new_num;");
	END IF;
                  
-- print chuoi kq;
 -- select strSQL;
	set @sqlexc :=strSQL;
	PREPARE dynamic_statement FROM @sqlexc;
	IF (in_PlantName = "" ) THEN
		BEGIN
			EXECUTE dynamic_statement;
			DEALLOCATE PREPARE dynamic_statement;
           -- select "kq-1";
		END;
	ELSE
		BEGIN
			IF (in_Initial = "" AND in_Area = "" AND in_TxtFreeWord = "" AND in_TL = false) THEN
				BEGIN
                    SET @PlantName_val = in_PlantName;
					EXECUTE dynamic_statement USING @PlantName_val;
					DEALLOCATE PREPARE dynamic_statement;
                   -- select "kq-2";
				END;
			ELSE
				BEGIN
					IF (in_Area<> "" AND in_Initial = "" AND in_TxtFreeWord = "") THEN
						BEGIN
                            SET @PlantName_val = in_PlantName;
                            SET @Field_val = in_Area;
							EXECUTE dynamic_statement USING @PlantName_val, @Field_val;
							DEALLOCATE PREPARE dynamic_statement;
                           -- select "kq-3";
						END;
					ELSEIF (in_Area = "" AND in_Initial <> "" AND in_TxtFreeWord = "" ) THEN
						BEGIN
                            SET @PlantName_val = in_PlantName;
                            SET @PartId_val = in_Initial;
							EXECUTE dynamic_statement USING @PlantName_val, @PartId_val;
							DEALLOCATE PREPARE dynamic_statement;
                           -- select "kq-4";
						END;
					ELSEIF (in_Area = "" AND in_Initial = "" AND in_TxtFreeWord <> "" ) THEN
						BEGIN
							SET @PlantName_val = in_PlantName;
                            SET @txtFreeWord_val = in_TxtFreeWord;
							EXECUTE dynamic_statement USING @PlantName_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val;
							DEALLOCATE PREPARE dynamic_statement;
                           -- select "kq-5";
						END;
					ELSEIF (in_Area <> "" AND in_Initial = "" AND in_TxtFreeWord <> "" ) THEN
						BEGIN
							SET @PlantName_val = in_PlantName;
                            SET @Field_val = in_Area;
                            SET @txtFreeWord_val = in_TxtFreeWord;
							EXECUTE dynamic_statement USING @PlantName_val, @Field_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val;
							DEALLOCATE PREPARE dynamic_statement;
                           -- select "kq-6";
						END;
					ELSEIF (in_Area = "" AND in_Initial <> "" AND in_TxtFreeWord <> "" ) THEN
						BEGIN
							SET @PlantName_val = in_PlantName;
                            SET @PartId_val = in_Initial;
                            SET @txtFreeWord_val = in_TxtFreeWord;
							EXECUTE dynamic_statement USING @PlantName_val, @PartId_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val;
							DEALLOCATE PREPARE dynamic_statement;
                           -- select "kq-7";
						END;
					ELSE
						BEGIN
							SET @PlantName_val = in_PlantName;
                            SET @Field_val = in_Area;
                            SET @PartId_val = in_Initial;
                            SET @txtFreeWord_val = in_TxtFreeWord;
							EXECUTE dynamic_statement USING @PlantName_val, @Field_val, @PartId_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val,
															@txtFreeWord_val, @txtFreeWord_val;
							DEALLOCATE PREPARE dynamic_statement;
                           -- select "kq-8";
						END;
					END IF;
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
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Obs_Extend_List_02` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Obs_Extend_List_02`(in_PlantName varchar(10), in_Lang varchar(1))
BEGIN
    declare strSQL text;
    set strSQL := "SELECT tbl_obs_extend.* ";
/*
        -- in_Lang: "ｵﾘｼﾞﾅﾙ版／Original";"0";"日本語版／Japanese";"1";"英語版／English";"2"
*/
    If in_Lang = '2' Then
        set strSQL := CONCAT(strSQL , ", IF(tbl_obs_extend.title_trans = '', 'making', tbl_obs_extend.title_trans) AS l_title"); -- Lタイトル
        set strSQL := CONCAT(strSQL , ", IF(val_comp = True, 'Complete', 'Incomplete') AS val_status"); -- VAL状況
        set strSQL := CONCAT(strSQL , ", IF(trans_range = 0, 'Nothing', IF(trans_range = 1, 'Partial', IF(trans_range = '2', 'Whole', ''))) AS l_trans_range"); -- L翻訳範囲
        set strSQL := CONCAT(strSQL , ", IF(approval_state_pr=True,");
        set strSQL := CONCAT(strSQL , "  CONCAT( '【Reviewer】' , Fun_Jap_Eng_Con_App_Status('E',approval_state_pr_state)),");
        set strSQL := CONCAT(strSQL , "  CONCAT( IF(approval_state1=True,'【As.1】','') ,  IF(approval_state2=True,'【As.2】','') , IF(approval_state3=True,'【As.3】','') , IF(approval_state_tl=True,'【TL】','') , Fun_Jap_Eng_Con_App_Status('E',approval_state_tl_state) )) AS tl_approval_status");-- TL承認状況
    Else
        set strSQL := CONCAT(strSQL , ", IF(english_edition = 'E',");
        set strSQL := CONCAT(strSQL , " tbl_obs_extend.title_trans,");
        set strSQL := CONCAT(strSQL , " tbl_obs_extend.title) AS l_title"); -- Lタイトル
        set strSQL := CONCAT(strSQL , ", IF(english_edition = 'E',");
        set strSQL := CONCAT(strSQL , " IF(val_comp = True, 'Complete', 'Incomplete'),");
        set strSQL := CONCAT(strSQL , " IF(val_comp = True, '完了', '未完')) AS val_status");-- VAL状況
        set strSQL := CONCAT(strSQL , ", IF(english_edition = 'E',");
        set strSQL := CONCAT(strSQL , " IF(trans_range = 0, 'Nothing', IF(trans_range = 1, 'Partial', IF(trans_range = '2', 'Whole', ''))),");
        set strSQL := CONCAT(strSQL , " IF(trans_range = 0, 'なし', IF(trans_range = '1', '部分', IF(trans_range = '2', '全文', '')))) AS l_trans_range"); -- L翻訳範囲
        set strSQL := CONCAT(strSQL , ", IF(approval_state_pr=True,");
        set strSQL := CONCAT(strSQL , "  CONCAT (IF(english_edition ='E','【Reviewer】','【レビュワ】') , Fun_Jap_Eng_Con_App_Status(english_edition,approval_state_pr_state)), ");
        set strSQL := CONCAT(strSQL , "  CONCAT (IF(approval_state1=True,IF(english_edition ='E','【As.1】','【審1】'),'') , IF(approval_state2=True,IF(english_edition='E','【As.2】','【審2】'),'') , ");
        set strSQL := CONCAT(strSQL , "  IF(approval_state3=True,IF(english_edition='E','【As.3】','【審3】'),'') , IF(approval_state_tl=True,'【TL】','') , Fun_Jap_Eng_Con_App_Status(english_edition,approval_state_tl_state))) AS tl_approval_status");-- TL承認状況
    End If;
    set strSQL := CONCAT(strSQL , " FROM tbl_obs_extend");
    set strSQL := CONCAT(strSQL , " WHERE tbl_obs_extend.plant_name = ? "); -- 1.in_PlantName
    set strSQL := CONCAT(strSQL , " ORDER BY tbl_obs_extend.field_order, tbl_obs_extend.new_num;"); -- 分野並び, 番号
	-- print select
    -- select strSQL;
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
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Obs_List_For_Req_Tran2`(in_PlantName varchar(10), in_Kinds varchar(3), in_Field varchar(30), in_PartId varchar(3), in_SerialNum int, in_Revisions int, in_Lang varchar(1))
BEGIN
	DECLARE strSQL TEXT;
	SET strSQL := "SELECT id, plant_name, kinds, fields, part_id, serial_num, revisions, english_edition, title,";
	SET strSQL := Concat(strSQL," title_trans, editor, approval_state_pr_rev, trans_range, trans_deadline, trans_lang, last_update");
	SET strSQL := Concat(strSQL," FROM tbl_obs WHERE trans_state_req = true");
	SET strSQL := Concat(strSQL," AND	translated_state = false ");
	SET strSQL := Concat(strSQL," AND plant_name = ?"); -- 1 {obsDto.PlantName}
	SET strSQL := Concat(strSQL," AND hold = -1 AND delete_flag = 1 AND trans_range = (1 OR 2) ");
	SET strSQL := Concat(strSQL," AND kinds = ? "); -- 2{obsDto.Kinds}
	SET strSQL := Concat(strSQL," AND fields = ? "); -- 3{obsDto.Fields}
	SET strSQL := Concat(strSQL," AND part_id = ? "); -- 4{obsDto.PartId}
	SET strSQL := Concat(strSQL," AND serial_num = ? "); -- 5{obsDto.SerialNum}
	SET strSQL := Concat(strSQL," AND revisions = ? ");  -- 6{obsDto.Revisions}
	SET strSQL := Concat(strSQL," AND english_edition = ?"); -- 7{obsDto.EnglishEdition}
	SET strSQL := Concat(strSQL," AND NOT EXISTS (SELECT * FROM tbl_translate ");
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
	-- print select
    -- select strSQL;
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
			SET strSQl := "SELECT tbl_reviewmember.respns_area as RespnsArea,
								  tbl_reviewmember.language as Language,
								  tbl_reviewmember.coordinator as Coordinator,
								  tbl_reviewmember.TL,
								  tbl_reviewmember.judge as Judge ,
								  tbl_reviewmember.trans as Trans
      						FROM tbl_peerreview
							INNER JOIN tbl_reviewmember";
			IF(in_IsExistsField = true) THEN
				SET strSQl := concat(strSQl," ON tbl_peerreview.plant_name = tbl_reviewmember.plant_name ");
			ELSE
				SET strSQl := concat(strSQl," ON tbl_peerreview.id = tbl_reviewmember.id_plant ");
			END IF;
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
-- print chuoi kq;
-- select strSQL;
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
-- select 'ok';
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Sp_Get_Tbl_Obs_Extend_Refer` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Get_Tbl_Obs_Extend_Refer`(in_Lang varchar(1))
BEGIN
declare strSQL text;
	set strSQL :="
SELECT 
        tbl_obs.*,
        CONCAT(tbl_obs.plant_name,
                '-',
                `tbl_obs`.kinds,
                '-',
                tbl_obs.fields,
                '-',
                tbl_obs.part_id,
                '-',
                LPAD( tbl_obs.serial_num,2,'0'),
                '-r',
                tbl_obs.revisions,
                IF((tbl_obs.english_edition = 'E'),
                    '-E',
                    '')) AS num,
		If(tbl_obs.approval_state_pr = True, 
			 concat( If(? = 'E','【Reviewer】','【レビュワ】') , tbl_obs.approval_state_pr_state),
			 concat( If(tbl_obs.approval_state1=true, If(? = 'E','【As.1】','【審1】'),"") , 
					 If(tbl_obs.approval_state2=true, If(? = 'E','【As.2】','【審2】'),"") ,
					 If(tbl_obs.approval_state3=true, If(? = 'E','【As.3】','【審3】'),"") , 
					 If(tbl_obs.approval_state_tl=true,'【TL】',"") , 
					 Nz(tbl_obs.approval_state_tl_state)) 
		  ) AS approval_status_tl,
       (SELECT 
                tbl_field.output_order
            FROM
                tbl_field
            WHERE
                ((tbl_field.plant_name = tbl_obs.plant_name)
                    AND (tbl_field.fields = tbl_obs.fields))
            LIMIT 1) AS field_order,
       (SELECT 
                tbl_translate.trans_situation
            FROM
                tbl_translate
            WHERE
                (tbl_translate.offer_num = (SELECT 
                        MAX(tbl_translate.offer_num)
                    FROM
                        tbl_translate
                    WHERE
                        (CONCAT(tbl_translate.plant_name,
                                '-',
                                tbl_translate.num) = Num)))
            LIMIT 1) AS trans_situation,
        (SELECT 
                tbl_translate.cansel_status
            FROM
                tbl_translate
            WHERE
                (tbl_translate.offer_num = (SELECT 
                        MAX(tbl_translate.offer_num)
                    FROM
                        tbl_translate
                    WHERE
                        (CONCAT(tbl_translate.plant_name,
                                '-',
                                tbl_translate.num) = Num)))
            LIMIT 1) AS trans_cansel,
        (SELECT 
                tbl_obs.revisions
            FROM
                tbl_obs tblObs
            WHERE
                ((tblObs.plant_name = tbl_obs.plant_name)
                    AND (tblObs.kinds = `tbl_obs`.kinds)
                    AND (tblObs.fields = tbl_obs.fields)
                    AND (tblObs.part_id = tbl_obs.`part_id`)
                    AND (tblObs.serial_num = tbl_obs.serial_num)
                    AND (tblObs.revisions <= tbl_obs.revisions)
                    AND (tblObs.delete_flag = FALSE)
                    AND (tblObs.translated_state = TRUE))
            ORDER BY CONCAT(tblObs.plant_name,
                    '-',
                    tblObs.kinds,
                    '-',
                    tblObs.fields,
                    '-',
                    tblObs.part_id,
                    '-',
                    LPAD( tblObs.serial_num,2,'0'),
                    '-r',
                    tblObs.revisions,
                    IF((tblObs.english_edition = 'E'),
                        '-E',
                        '')) DESC
            LIMIT 1) AS past_translated_rev,
        (SELECT 
                tbl_obs.translated_state
            FROM
                tbl_obs tblObs
            WHERE
                ((tblObs.plant_name = tbl_obs.plant_name)
                    AND (tblObs.kinds = `tbl_obs`.kinds)
                    AND (tblObs.fields = tbl_obs.fields)
                    AND (tblObs.part_id = tbl_obs.`part_id`)
                    AND (tblObs.serial_num = tbl_obs.serial_num)
                    AND (tblObs.revisions <= tbl_obs.revisions)
                    AND (tblObs.delete_flag = FALSE)
                    AND (tblObs.translated_state = TRUE))
            ORDER BY CONCAT(tblObs.plant_name,
                    '-',
                    tblObs.kinds,
                    '-',
                    tblObs.fields,
                    '-',
                    tblObs.part_id,
                    '-',
                    LPAD( tblObs.serial_num,2,'0'),
                    '-r',
                    tblObs.revisions,
                    IF((tblObs.english_edition = 'E'),
                        '-E',
                        '')) DESC
            LIMIT 1) AS past_translated_status
    FROM
        tbl_obs
    WHERE
            tbl_obs.delete_flag = false	
	ORDER BY CONCAT(tbl_obs.plant_name,
					'-',
					`tbl_obs`.kinds,
					'-',
					tbl_obs.fields,
					'-',
					tbl_obs.part_id,
					'-',
                    LPAD( tbl_obs.serial_num,2,'0'),
					'-r',
					tbl_obs.revisions,
					IF((tbl_obs.english_edition = 'E'),
						'-E',
						'')
                    );";
	set @sqlexc :=strSQL;
   PREPARE dynamic_statement FROM @sqlexc;
   SET @lang_val = in_Lang;
   EXECUTE dynamic_statement USING @lang_val, @lang_val, @lang_val, @lang_val;
   DEALLOCATE PREPARE dynamic_statement;
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
CREATE DEFINER=`root`@`%` PROCEDURE `Sp_Set_Data_List_To_Print`(in_SheetName varchar(1000), in_CallForm varchar(1000),
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
			SET strTableName := "tbl_obs_extend";
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
	-- select "in_CallForm", in_CallForm;
	IF in_CallForm = "frm_ADMenu" THEN

		SET strSQL := CONCAT("SELECT " , strTableName , ".id");
		SET strSQL := CONCAT(strSQL, ", " , strTableName , ".new_num");
		SET strSQL := CONCAT(strSQL, ", " , strTableName , ".fields");
        
		-- in_Lang: "ｵﾘｼﾞﾅﾙ版／Original";"0";"日本語版／Japanese";"1";"英語版／English";"2"
		CASE in_Lang
			WHEN 1 THEN
				SET strSQL := CONCAT(strSQL, ", " , strTableName , ".title");
			WHEN 2 THEN
				SET strSQL := CONCAT(strSQL, ", " , strTableName , ".title_trans");
			ELSE
				SET strSQL := CONCAT(strSQL, ", If(", strTableName , ".english_edition = 'E', " , strTableName , ".title_trans, null ) AS title_trans ");
                SET strSQL := CONCAT(strSQL, ", If(", strTableName , ".english_edition <> 'E', " , strTableName , ".title, null ) AS title");
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
									   null as approval_state_tl_id,null as approval_state_tl_rev,null as approval_state_tl_state,null as english_edition,
									   null as hold,null as newest_flag,null as delete_flag,null as editor,
									   null as last_update,null as last_user,null as l_num,
									   null as field_order,null as trans_situation,null as trans_cansel,null as past_translated_rev,
									   null as past_translated_status,null as l_title,null as val_status,null as l_trans_range,
									   null as tl_approval_status");
		SET strSQL := CONCAT(strSQL,  " FROM " , strTableName);
		SET strSQL := CONCAT(strSQL,  " WHERE (" , strTableName , ".newest_flag = -1)");
        SET strSQL := CONCAT(strSQL,  " AND (" , strTableName , ".plant_name = ?)");
		SET strSQL := CONCAT(strSQL, " AND (" , strTableName , ".delete_flag = 0)");

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
		-- Call store of Form_frm_PastData_Search.L_strSQL
		Call Sp_Get_Data_Null_ForPrint();
	ELSE
	    CASE in_SheetName
			WHEN "GFA" THEN
				-- Call store of Form_frm_GFA_List.L_strSQL
				Call Sp_Get_Data_Null_ForPrint();
			WHEN "PFA" THEN
				-- Call store of Form_frm_PFA_List.L_strSQL
				Call Sp_Get_Data_Null_ForPrint();
			WHEN "AFI" THEN
				-- Call store of Form_frm_AFI_List.L_strSQL
				Call Sp_Get_Data_Null_ForPrint();
			WHEN "BP" THEN
				-- Call store of Form_frm_BP_List.L_strSQL
				Call Sp_Get_Data_Null_ForPrint();
			WHEN "OBS" THEN
				-- strSQL = Form_frm_OBS_List.L_strSQL
                -- select "obs";
				CALL Sp_Get_Obs_Extend_List_01 (in_PlantName, in_Area, in_Initial, in_Lang, in_TL, in_TxtFreeWord);
			WHEN "PD" THEN
				-- Call store of Form_frm_PD_List.L_strSQL
				Call Sp_Get_Data_Null_ForPrint();
			WHEN "STR" THEN
				-- Call store of Form_frm_STR_List.L_strSQL
				Call Sp_Get_Data_Null_ForPrint();
			WHEN "SOI" THEN
				-- Call store of Form_frm_SOI_List.L_strSQL
				Call Sp_Get_Data_Null_ForPrint();
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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-07-19 12:56:10
