SELECT
									ord.OrderId
									, ord.BussinessCd
									, ord.BarCd
									, ord.TotalQuantity
									, mc.CustId
									, mc.KanjiFamilyName
									, mc.KanjiFirstName
									, mc.KanaFamilyName
									, mc.KanaFirstName
									, mc.PhoneNumber
									, mc.MailAddress
									, (SELECT  group_concat(DISTINCT(mbr.BrandCd))
												FROM orderdetail ordd
												INNER JOIN mproductdetail mpd
													ON ordd.Sku = mpd.Sku
													AND mpd.DelFlg = 0
												INNER JOIN mproduct mp
													ON mpd.ProductCd = mp.ProductCd
													AND mp.DelFlg = 0
												INNER JOIN mbrandrelation mbr
													ON mbr.BrandRltId = mp.BrandRltId
													AND mbr.DelFlg = 0
												WHERE ordd.OrderId = ord.OrderId
													AND mbr.BussinessCd = ord.BussinessCd
												GROUP BY ordd.OrderId, mbr.BrandCd ) as BrandCd
									, (SELECT group_concat(DISTINCT(mb.BrandName))
												FROM orderdetail ordd
												INNER JOIN mproductdetail mpd
													ON ordd.Sku = mpd.Sku
													AND mpd.DelFlg = 0
												INNER JOIN mproduct mp
													ON mpd.ProductCd = mp.ProductCd
													AND mp.DelFlg = 0
												INNER JOIN mbrandrelation mbr
													ON mbr.BrandRltId = mp.BrandRltId
													AND mbr.DelFlg = 0
												INNER JOIN mbrand mb
													ON mbr.BrandCd = mb.BrandCd
													AND mb.DelFlg = 0
												WHERE ordd.OrderId = ord.OrderId
												 AND mbr.BussinessCd = ord.BussinessCd
												GROUP BY ordd.OrderId )  as BrandName
									, adrMc.ZipCd
									, adrMc.ZipCodeDsp
									, adrMc.Province
									, kbnProvinceMc.KbnValueName AS ProvinceName
									, adrMc.AdrCd1
									, adrMc.AdrCd2
									, adrMc.AdrCd3
									, ord.ShopCd
									, ms.ShopName
									, ms.Area2Cd
									, ord.Total
									, adrr.KanjiFamilyNameMCustReceive
									, adrr.KanjiFirstNameMCustReceive
									, adrr.KanaFamilyNameMCustReceive
									, adrr.KanaFirstNameMCustReceive
									, adrr.PhoneNumberMCustReceive
									, adrr.ZipCdMCustReceive
									, adrr.ZipCodeDspMCustReceive
									, adrr.ProvinceMCustReceive
									, adrr.ProvinceNameMCustReceive
									, adrr.AdrCd1MCustReceive
									, adrr.AdrCd2MCustReceive
									, adrr.AdrCd3MCustReceive
									, adrr.CreateUserCdCustRecieve
									, adrr.UpdateDtTmCustRecieve
									, adrr.UpdateDtTmAddress
									, ord.ReceiveWay
									, CASE
										WHEN ord.ReceiveWay = '01' OR ord.ReceiveWay IS NULL THEN '店頭受取'
										WHEN ord.ReceiveWay ='02' THEN '配送'
										ELSE ''
									END AS ReceiveWayName
									, ord.PaymentWay
									, CASE
										WHEN ord.PaymentWay = '01' THEN '店舗で支払い'
										WHEN ord.PaymentWay ='02' THEN '振込'
										ELSE ''
									END AS PaymentWayName
									, ord.IsDiscount
									, CASE ord.IsDiscount
										WHEN 0 THEN 'なし'
										WHEN 1 THEN 'あり'
										ELSE ''
									END AS DiscountName
									, ord.Memo
									, ord.Status
									, CASE ord.Status
										WHEN '01' THEN '予約(入金待ち)'
										WHEN '02' THEN '入金完了(引渡し待ち）'
										WHEN '04' THEN '引渡し完了'
										WHEN '05' THEN 'キャンセル'
									END AS StatusName
									, ord.CreateDtTm
									, ord.UpdateDtTm
									, CASE WHEN sm_order.MailType = '10'
													AND sm_order.MailStatus is not null
												THEN sm_order.MailStatus
											ELSE '04'
										END AS OrderMailStatus
									, CASE WHEN sm_order.MailType = '10'
													AND sm_order.MailStatus = '01' 
												THEN '送信済'
											WHEN sm_order.MailType = '10'
													AND sm_order.MailStatus = '02' 
												THEN '送信エラー'
											WHEN sm_order.MailType = '10'
													AND sm_order.MailStatus = '03'
												THEN '再送'
												ELSE '未送信'
											END AS OrderMailStatusName
									, CASE WHEN sm_remind.MailType = '20'
													AND sm_remind.MailStatus is not null
												THEN sm_remind.MailStatus
											ELSE '04'
										END AS RemindMailStatus
									, CASE WHEN sm_remind.MailType = '20'
													AND sm_remind.MailStatus = '01'
												THEN '送信済'
											WHEN sm_remind.MailType = '20'
													AND sm_remind.MailStatus = '02'
												THEN '送信エラー'
											WHEN sm_remind.MailType = '20'
													AND sm_remind.MailStatus = '03'
												THEN '再送'
												ELSE '未送信'
											END AS RemindMailStatusName
									, CASE WHEN sm_finished.MailType = '30'
													AND sm_finished.MailStatus is not null
												THEN sm_finished.MailStatus
											ELSE '04'
										END AS FinishedMailStatus
									, CASE WHEN sm_finished.MailType = '30'
													AND sm_finished.MailStatus = '01'
												THEN '送信済'
											WHEN sm_finished.MailType = '30'
													AND sm_finished.MailStatus = '02'
												THEN '送信エラー'
											WHEN sm_finished.MailType = '30'
													AND sm_finished.MailStatus = '03'
												THEN '再送'
												ELSE '未送信'
											END AS FinishedMailStatusName
									, sm_mailbounce.ErrorCd AS MailBounce
									, CASE WHEN sm_mailbounce.MailStatus = '02'
										THEN CASE WHEN sm_mailbounce.ErrorCd = '5.1.1'
													THEN 'メールアドレスが存在しない・無効/不正'
												WHEN sm_mailbounce.ErrorCd = '5.2.2'
													THEN 'メールボックスがいっぱい'
												WHEN sm_mailbounce.ErrorCd = '5.6.1'
													THEN 'メールが拒否された'
												WHEN sm_mailbounce.ErrorCd IS NOT NULL
													THEN 'その他'
												END
									END AS MailBounceName
									, sm_mailbounce.ErrorDescription AS MailBounceDescription
									, ord.CreateUserCd
									, ord.UpdateDtTm  FROM orders ord
							
							INNER JOIN mcust mc
								ON ord.CustId = mc.CustId 
									AND mc.DelFlg = 0
							
							INNER JOIN maddress adrMc
								ON mc.AddrId = adrMc.AddrId
								AND adrMc.AdrType = '1'
								AND adrMc.DelFlg = 0
							INNER JOIN mkbnvalue kbnProvinceMc
								ON kbnProvinceMc.KbnCd = '0001'
								AND kbnProvinceMc.KbnValue = adrMc.Province
								AND kbnProvinceMc.DelFlg = 0
							INNER JOIN sendmail sm_order
								ON ord.OrderId = sm_order.OrderId
								AND sm_order.MailType = '10'
								AND sm_order.SendTo = '01'
								AND sm_order.DelFlg = 0
							LEFT JOIN mshop ms
								ON ord.ShopCd = ms.ShopCd
									AND ms.DelFlg = 0
							LEFT JOIN (SELECT
											mcr.OrderId
											, mcr.KanjiFamilyName AS KanjiFamilyNameMCustReceive
											, mcr.KanjiFirstName AS KanjiFirstNameMCustReceive
											, mcr.KanaFamilyName AS KanaFamilyNameMCustReceive
											, mcr.KanaFirstName AS KanaFirstNameMCustReceive
											, mcr.PhoneNumber AS PhoneNumberMCustReceive
											, adr.ZipCd AS ZipCdMCustReceive
											, adr.ZipCodeDsp AS ZipCodeDspMCustReceive
											, adr.Province AS ProvinceMCustReceive
											, kbnProvince.KbnValueName AS ProvinceNameMCustReceive
											, adr.AdrCd1 AS AdrCd1MCustReceive
											, adr.AdrCd2 AS AdrCd2MCustReceive
											, adr.AdrCd3 AS AdrCd3MCustReceive
											, mcr.CreateUserCd AS CreateUserCdCustRecieve
											, mcr.UpdateDtTm AS UpdateDtTmCustRecieve
											, adr.UpdateDtTm AS UpdateDtTmAddress
										FROM mcustrecieve mcr
										INNER JOIN orders ordSub
											ON mcr.OrderId = ordSub.OrderId
											AND ordSub.DelFlg = 0
										INNER JOIN maddress adr
											ON mcr.AddrId = adr.AddrId
												AND adr.DelFlg = 0
										INNER JOIN mkbnvalue kbnProvince
											ON kbnProvince.KbnCd = '0001'
											AND kbnProvince.KbnValue = adr.Province
											AND kbnProvince.DelFlg = 0
										WHERE adr.AdrType = '3'
											 AND ordSub.BussinessCd = @BussinessCd
											AND adr.DelFlg = 0) AS adrr
								ON ord.OrderId = adrr.OrderId
							 LEFT JOIN sendmail sm_remind
									ON ord.OrderId = sm_remind.OrderId
									AND sm_remind.MailType = '20'
									AND sm_remind.SendTo = '01'
									AND sm_remind.DelFlg = 0
								LEFT JOIN sendmail sm_finished
									ON ord.OrderId = sm_finished.OrderId
									AND sm_finished.MailType = '30'
									AND sm_finished.SendTo = '01'
									AND sm_finished.DelFlg = 0
							 LEFT JOIN (SELECT
														sm_lastest.OrderId
														, sm_lastest.MailStatus
														, sm_lastest.ErrorCd
														, sm_lastest.ErrorDescription
													FROM
														(SELECT
															OrderId
															, SendTo
															, MailStatus
															, ErrorCd
															, ErrorDescription
															, MAX(UpdateDtTm) UpdateDtTm
														FROM
															sendmail
														WHERE SendTo = '01'
															
														GROUP BY OrderId) sm_max
													INNER JOIN
														sendmail sm_lastest USING (OrderId, SendTo, UpdateDtTm)
													) sm_mailbounce
											ON ord.OrderId = sm_mailbounce.OrderId
											AND sm_mailbounce.MailStatus = '02'
							WHERE 1=1
								 AND ord.BussinessCd = '1' AND ord.DelFlg = 0  ORDER BY ord.UpdateDtTm DESC  LIMIT 10 OFFSET 0