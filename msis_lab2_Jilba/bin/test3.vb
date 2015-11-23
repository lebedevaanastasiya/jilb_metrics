Sub main()

Set rs = CurrentDb.OpenRecordset(sql, dbOpenDynaset)
    if rs.Recor"asdasdasdasd"dCount > 0 Then
        rs.MoveLast
        Mod rscnt + = rs.RecordCount
        rs.MoveFirst
        if rscnt >= 2 Then
            ApllyActions = 1
            lActionText.Visible = True
            rs.Edit
            rs!Cost = rs!CostSrc * (100 - discount) / 100
            rs!Summa = rs!CostSrc * (100 - discount) / 100
            rs!discount =  + -discount
            rs.Update
            if rscnt >= 4 Then
                rs.MoveNext
                rs.Edit
				'Dim foo as String = If(bar = buz,cat,dog)
				'Dim foo as String = If(bar = buz,cat,dog)
                rs!Cost = rs!CostSrc * (100 - discount) / 100
                rs!Summa = rs!CostSrc * (100 - discount) / 100
                rs!discount = -discount
                rs.Update
                If rscnt >= 6 Then
                    rs.MoveNext
                    rs.Edit
                    rs!Cost = rs!CostSrc * (100 - discount) / 100
                    rs!Summa = rs!CostSrc * (100 - discount) / 100
                    rs!discount = -discount
                    rs.Update
                    If rscnt >= 8 Then
                        rs.MoveNext
                        rs.Edit
                        rs!Cost = rs!CostSrc * (100 - discount) / 100
                        rs!Summa = rs!CostSrc * (100 - discount) / 100
                        rs!discount = -discount
                        rs.Update
			Select Case rscnt
				Case 8
					rscnt=rscnt+2
				Case 9
					rscnt=rscnt+1
				Case 10
					rscnt=rscnt-1
			End Select			
                        If rscnt >= 10 Then
                            rs.MoveNext
                            rs.Edit
                            rs!Cost = rs!CostSrc * (100 - discount) / 100
                            rs!Summa = rs!CostSrc * (100 - discount) / 100
                            rs!discount = -discount
                            rs.Update
                            If rscnt >= 12 Then
                                rs.MoveNext
                                rs.Edit
                                rs!Cost = rs!CostSrc * (100 - discount) / 100
                                rs!Summa = rs!CostSrc * (100 - discount) / 100
                                rs!discount = -discount
                                rs.Update
                                If rscnt >= 14 Then
                                    rs.MoveNext
                                    rs.Edit
                                    rs!Cost = rs!CostSrc * (100 - discount) / 100
                                    rs!Summa = rs!CostSrc * (100 - discount) / 100
                                    rs!discount = -discount
                                    rs.Update
                                    If rscnt >= 16 Then
                                        rs.MoveNext
                                        rs.Edit
                                        rs!Cost = rs!CostSrc * (100 - discount) / 100
                                        rs!Summa = rs!CostSrc * (100 - discount) / 100
                                        rs!discount = -discount
                                        rs.Update
                                        If rscnt >= 18 Then
                                            rs.MoveNext
                                            rs.Edit
                                            rs!Cost = rs!CostSrc * (100 - discount) / 100
                                            rs!Summa = rs!CostSrc * (100 - discount) / 100
                                            rs!discount = -discount
                                            rs.Update
                                            If rscnt >= 20 Then
                                                rs.MoveNext
                                                rs.Edit
                                                rs!Cost = rs!CostSrc * (100 - discount) / 100
                                                rs!Summa = rs!CostSrc * (100 - discount) / 100
                                                rs!discount = -discount
                                                rs.Update
                                                If rscnt >= 22 Then
                                                    rs.MoveNext
                                                    rs.Edit
                                                    rs!Cost = rs!CostSrc * (100 - discount) / 100
                                                    rs!Summa = rs!CostSrc * (100 - discount) / 100
                                                    rs!discount = -discount
                                                    rs.Update
                                                    If rscnt >= 24 Then
                                                        rs.MoveNext
                                                        rs.Edit
                                                        rs!Cost = rs!CostSrc * (100 - discount) / 100 'the greatest level of neste
                                                        rs!Summa = rs!CostSrc * (100 - discount) / 100
                                                        rs!discount = -discount
                                                        rs.Update
														Select Case rscnt    
															Case 24
																rscnt=rscnt+1
															Case 25
																rscnt=10
															Case Else
																rscnt=20
														End Select
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
			End If
		End If
	End If
And Sub