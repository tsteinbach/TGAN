select * from Ansetzung where SpieltagID = (Select ID from Spieltag where Spieltag = 1 and SeasonID = (select ID from Season where Season = '2007/2008'))

Update Ansetzung set result = '0:0&nbsp;(-:-)' where Spiel = 2 and SpieltagID = (Select ID from Spieltag where Spieltag = 1 and SeasonID = (select ID from Season where Season = '2007/2008'))


select * from Tipp where AnsetzungID = 
	(select ID from Ansetzung where Spiel = 2 and SpieltagID =
(Select ID from Spieltag where Spieltag = 1 and SeasonID = 
(select ID from Season where Season = '2007/2008')))


