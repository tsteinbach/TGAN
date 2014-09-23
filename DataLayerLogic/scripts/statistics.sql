--'Home','Away','NotSet','Draw'

-- Gruppierung nach Tipptendenz
select m.[User],s.Season,t.Tipp, COUNT(0) as 'Count Tendenz'
from Tipp t join member m on t.MemberID = m.ID 
join Ansetzung a on a.ID = t.AnsetzungID
join Spieltag sp on sp.ID = a.SpieltagID
join Season s on s.ID = sp.SeasonID
group by [User],s.Season, t.Tipp 
order by [User]

-- Grupperiung nach Echte Bank tipps
select m.[User],s.Season,count(g.EchteBank) as 'Count Echte Bank'
from GesamtstandPerRound g join member m on g.MemberID = m.ID 
join Spieltag sp on sp.ID = g.SpieltagID
join Season s on s.ID = sp.SeasonID
where g.EchteBank <> 0
group by [User],s.Season 
order by [User]

-- Grupperiung nach UnEchte Bank tipps
select m.[User],s.Season,count(g.UnechteBank) as 'Count Unechte Bank'
from GesamtstandPerRound g join member m on g.MemberID = m.ID 
join Spieltag sp on sp.ID = g.SpieltagID
join Season s on s.ID = sp.SeasonID
where g.UnechteBank <> 0 
group by [User],s.Season 
order by [User]

-- Grupperiung nach neunertipp
select m.[User],s.Season,count(g.NeunerTipp) as 'Count Neuner'
from GesamtstandPerRound g join member m on g.MemberID = m.ID 
join Spieltag sp on sp.ID = g.SpieltagID
join Season s on s.ID = sp.SeasonID
where g.NeunerTipp <> 0 
group by [User],s.Season 
order by [User]

-- Gruppering nach Punkte insgesamt
select m.[User],s.Season,sum(g.PunkteInsgesamt) as 'Punkte insgesamt'
from GesamtstandPerRound g join member m on g.MemberID = m.ID 
join Spieltag sp on sp.ID = g.SpieltagID
join Season s on s.ID = sp.SeasonID
group by [User],s.Season 
order by [User]