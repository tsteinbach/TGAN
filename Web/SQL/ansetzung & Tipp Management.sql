
select sp.Spieltag, a.Spiel, a.Zeit as Anspielzeit, 
(select t.NameToShow from Teams t where t.ID = a.TeamID_home) as Heim,
(select t.NameToShow from Teams t where t.ID = a.TeamID_away) as Gast
from Ansetzung a join Spieltag sp on a.SpieltagID = sp.ID
join Season s on s.ID = sp.SeasonID
where s.Season = '2020/2021'
order by sp.Spieltag, a.Spiel

select * from Ansetzung where SpieltagID = (Select ID from Spieltag where Spieltag = 1 and SeasonID = (select ID from Season where Season = '2007/2008'))

Update Ansetzung set result = '0:0&nbsp;(-:-)' where Spiel = 2 and SpieltagID = (Select ID from Spieltag where Spieltag = 1 and SeasonID = (select ID from Season where Season = '2007/2008'))


select * from Tipp where AnsetzungID = 
	(select ID from Ansetzung where Spiel = 2 and SpieltagID =
(Select ID from Spieltag where Spieltag = 1 and SeasonID = 
(select ID from Season where Season = '2007/2008')))


