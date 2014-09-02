Update Ansetzung set TeamID_home  = 
	(select ID from Teams where NameToShow = Ansetzung.Heim_Team)
Update Ansetzung set TeamID_away  = 
	(select ID from Teams where NameToShow = Ansetzung.Gast_Team)




select distinct Heim_Team from Ansetzung			