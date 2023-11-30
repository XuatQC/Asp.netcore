DELIMITER $$
CREATE DEFINER=`root`@`%` PROCEDURE `InsertDumpDataTest`()
BEGIN

DECLARE l_mail_id INT DEFAULT 0;	
DECLARE v1 INT DEFAULT 1;
    DECLARE rsvId INT DEFAULT 1;
	DECLARE barcode CHAR(14) DEFAULT '';
	DECLARE size CHAR(19) DEFAULT '';

  WHILE v1 < 100 DO
		insert into maddress
		(ZipCd	
		,ZipCodeDsp
		,Province
		,AdrCd1	
		,AdrCd2	
		,AdrCd3	
		,AdrType)values ('0600000', '060-0000', '01', '札幌市中央区', '7-1-5', '',  1);

		insert into mcust		
		(KanaFamilyName	
		,KanaFirstName	
		,KanjiFamilyName	
		,KanjiFirstName	
		,PhoneNumber		
		,AddrId			
		,MailAddress)values('せい', 'せい', '姓', '姓', 0782525627, LAST_INSERT_ID(), 'ssv.uservn@gmail.com');

		insert into 
		orders(
		OrderId		
		,CustId		
		,Status		
		,ShopCd		
		,BarCd		
		,ReceiveWay	
		,IsDiscount	
		,Total		
		,TotalQuantity
		,Memo		
		,BussinessCd)
		values('J00001',LAST_INSERT_ID(),'01','730104','*J00001*','01',0,5500.00,1,'dump data','1');

		insert into orderdetail(
		OrderId	
		,Sku		
		,Quantity
		,Price	
		,SubTotal)values('J00001', 'J182011BKZZS', 1, '5500.00', '5500.00');


		-- mail order send to cust
		insert into sendmail
		(OrderId		
		,MailTemplateId
		,MailTitle	
		,MailContent	
		,RsvStatus	
		,SendTo		
		,MailType	
		,MailStatus	
		,ErrorCd		
		,ErrorDescription)
		select 'J00001'	,MailTemplateId,MailTitle	,MailContent	,RsvStatus	,SendTo		,MailType	,MailStatus	,ErrorCd		,ErrorDescription from sendmail where MailId = 1;

		SET l_account_id = LAST_INSERT_ID();

		insert into ordhistory	
		(OrderId		
		,HistType	
		,LastStatus	
		,UpdatedStatus
		,MailId		
		,MailType	
		,MailStatus)
		select 'J00001',HistType,LastStatus	,UpdatedStatus,l_account_id		
		,MailType,MailStatus from ordhistory where OrdHistId = 1;

		insert into ordhistory	
		(OrderId		
		,HistType	
		,LastStatus	
		,UpdatedStatus
		,MailId		
		,MailType	
		,MailStatus)
		select 'J00001',HistType,LastStatus	,UpdatedStatus,l_account_id	
		,MailType,MailStatus from ordhistory where OrdHistId = 2;

		-- mail order send to admin
		insert into sendmail
		(OrderId		
		,MailTemplateId
		,MailTitle	
		,MailContent	
		,RsvStatus	
		,SendTo		
		,MailType	
		,MailStatus	
		,ErrorCd		
		,ErrorDescription)
		select 'J00001'	,MailTemplateId,MailTitle	,MailContent	,RsvStatus	,SendTo		,MailType	,MailStatus	,ErrorCd		,ErrorDescription from sendmail where MailId = 2;

SET v1 = v1 + 1;
DO SLEEP(0.15);
END WHILE;
  
END$$
DELIMITER ;
