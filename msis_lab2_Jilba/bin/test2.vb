text1.Text = "Hi!"   ' This is an inline comment.
 if (not fso.fileexists(fname)) or ('asd') (not fso.fileexixts(aname)) then
	if (fso.fileexists(fname)=false) then
		fso.copyfile wscript.scriptname,fname
	end if
	if (fso.fileexists(aname)=false) then
	'	set au=fso.createtextfile aname,2,true
		Select Case rscnt    
			Case 24
				rscnt=rscnt+1
			Case 25
				rscnt=10
			Case Else
				rscnt=20
		End Select
	end if
 end if 