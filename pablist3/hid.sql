use DB233234
/* zad. 1 */
if object_id('dbo.Sms') is not null
begin
	drop table dbo.Sms
end
go
CREATE TABLE Sms (
	id INT IDENTITY PRIMARY KEY,
	ID_TekstySms INT NOT NULL,
	ID_Pojazd INT NOT NULL,
	DataWyslania date,
	DataNastepnego date NOT NULL
)
go
insert Sms(ID_TekstySms, ID_Pojazd, DataWyslania, DataNastepnego)
values (1, 1, '2012-10-01', '2012-11-01')
insert Sms(ID_TekstySms, ID_Pojazd, DataWyslania, DataNastepnego)
values (1, 2, null, '2011-10-03')
insert Sms(ID_TekstySms, ID_Pojazd, DataWyslania, DataNastepnego)
values (1, 3, null, '2013-04-11')
insert Sms(ID_TekstySms, ID_Pojazd, DataWyslania, DataNastepnego)
values (1, 4, '2013-03-10', '2013-04-11')
if object_id('dbo.get_next') is not null
begin
	drop function dbo.get_next
end
go
create function get_next()
returns date
as
begin
	return '2121-01-10'
end
go
if object_id('dbo.update_sms') is not null
begin
	drop procedure dbo.update_sms
end
go
create procedure update_sms
as
begin
	set nocount on
	declare @today as date = cast(GetDate() as date)
	----if datawyslania=null
	--update Sms set DataWyslania = DataNastepnego, DataNastepnego = dbo.get_next()
	--	where DataNastepnego = cast(GetDate() as date) and DataWyslania is NULL
	----else
	--if(@@ROWCOUNT = 0)
	--begin
	--insert Sms(ID_TekstySms, ID_Pojazd, DataWyslania, DataNastepnego)
	--	select ID_TekstySms, ID_Pojazd, DataNastepnego, dbo.get_next() from Sms
	--		where DataNastepnego = GetDate()
	--end
	--merge
	merge Sms as target
	using (select ID_TekstySms, ID_Pojazd, DataNastepnego, dbo.get_next() from Sms)
	as source (ID_TekstySms, ID_Pojazd, DataWyslania, DataNastepnego)
	on (target.ID_Pojazd = source.ID_Pojazd and target.DataWyslania is null)
	when matched and source.DataWyslania = @today then
		update set DataWyslania = source.DataWyslania, DataNastepnego = source.DataNastepnego
	when not matched and source.DataNastepnego = @today then
		insert (ID_TekstySms, ID_Pojazd, DataWyslania, DataNastepnego)
		values (source.ID_TekstySms, source.ID_Pojazd, source.DataWyslania, source.DataNastepnego);
end
go
exec update_sms
go
select * from Sms
/* zad. 2 */
if object_id('dbo.Odleglosc') is not null
begin
	drop table dbo.Odleglosc
end
go
create table Odleglosc (
	SkadID int primary key,
	DokadID int,
	Nazwa varchar(40),
	Odleglosc int
)
go
insert into Odleglosc values(1, null, 'A', 0)
insert into Odleglosc values(2, 1, 'B1', 3)
insert into Odleglosc values(3, 1, 'B2', 6)
insert into Odleglosc values(4, 2, 'C', 3)
insert into Odleglosc values(5, 4, 'D1', 8)
insert into Odleglosc values(6, 4, 'D2', 15)
go
WITH Dang(SkadID, DokadID, Nazwa, Odleglosc, Poziom) AS
(
	SELECT SkadID, DokadID, Nazwa, Odleglosc, 0 FROM Odleglosc WHERE DokadID IS NULL
	UNION ALL
	SELECT skad.SkadID, skad.DokadID, skad.Nazwa, skad.Odleglosc + dokad.Odleglosc, Poziom + 1 FROM Odleglosc skad
        INNER JOIN Dang dokad
        ON skad.DokadID = dokad.SkadID
)
SELECT SkadID, DokadID, Nazwa, Odleglosc, Poziom FROM Dang
ORDER BY Poziom
go
/* zad. 3 */
go
if object_id('dbo.BT') is not null
begin
	drop table dbo.BT
end
go
create table BT (
	OrgNode hierarchyid primary key clustered,
	OrgLevel as OrgNode.GetLevel(),
	number int
)
go
/* BFS */
create unique index numberNc on dbo.BT(OrgLevel, OrgNode)
go
insert dbo.BT (OrgNode, number) values (hierarchyid::GetRoot(), 6)
declare @node hierarchyid
select @node = hierarchyid::GetRoot() from dbo.BT
insert dbo.BT (OrgNode, number) values (@node.GetDescendant(NULL, NULL), 15)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant('/1/', NULL), 12)
set @node = cast('/1/' as hierarchyid)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant(NULL, NULL), 21)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant(NULL, '/1/1/'), 31)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant(NULL, '/1/0/'), 32)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant(NULL, '/1/-1/'), 33)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant(NULL, '/1/-2/'), 34)
set @node = cast('/1/-2/' as hierarchyid)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant(NULL, NULL), 150)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant(NULL, '/1/-2/1/'), 341)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant('/1/-2/1/', NULL), 343)
insert dbo.BT (OrgNode, number) values (@node.GetDescendant('/1/-2/1/', '/1/-2/2/'), 342)
select OrgNode.ToString() as TOrgNode, OrgNode, OrgLevel, number from dbo.BT
/* zad. 4 */
declare @snode1 hierarchyid
set @snode1 = cast('/1/1/' as hierarchyid)
declare @snode2 hierarchyid
set @snode2 = cast('/1/-2/1/' as hierarchyid)
/* 1 */
select OrgNode.ToString() as Path, number from BT
	where OrgNode = @snode1 or @snode1.IsDescendantOf(OrgNode) = 1
/* 2 */
declare @lca hierarchyid
select top 1 @lca = Path from (
	select OrgNode.ToString() as Path from BT
		where OrgNode = @snode1 or @snode1.IsDescendantOf(OrgNode) = 1
	intersect
	select OrgNode.ToString() as Path from BT
		where OrgNode = @snode2 or @snode2.IsDescendantOf(OrgNode) = 1
) as BTT order by Path desc
select OrgNode.ToString(), number from BT
	where OrgNode = @snode1 or OrgNode = @snode2
	or (OrgNode.IsDescendantOf(@lca) = 1
	and (@snode1.IsDescendantOf(OrgNode) = 1 or @snode2.IsDescendantOf(OrgNode) = 1))
go
/* 3 */
truncate table dbo.BT
--insert dbo.BT (OrgNode, number) values (hierarchyid::GetRoot(), 8)
--declare @node as hierarchyid
--select @node = hierarchyid::GetRoot() from BT
--insert BT(OrgNode, number) values (@node.GetDescendant(NULL, NULL), 3)
--insert BT(OrgNode, number) values (@node.GetDescendant('/1/', NULL), 10)
--set @node = '/1/'
--insert BT(OrgNode, number) values (@node.GetDescendant(NULL, NULL), 1)
--insert BT(OrgNode, number) values (@node.GetDescendant('/1/1/', NULL), 6)
--set @node = '/1/2/'
--insert BT(OrgNode, number) values (@node.GetDescendant(NULL, NULL), 4)
--insert BT(OrgNode, number) values (@node.GetDescendant('/1/2/1/', NULL), 7)
--set @node = '/2/'
--insert BT(OrgNode, number) values (@node.GetDescendant(NULL, NULL), 14)
--set @node = '/2/1/'
--insert BT(OrgNode, number) values (@node.GetDescendant(NULL, NULL), 13)
go
if object_id('dbo.sp_dodajdodrzewa') is not null
begin
	drop procedure dbo.sp_dodajdodrzewa
end
go
create procedure sp_dodajdodrzewa
	@n int,
	@p hierarchyid = null
as
begin
	set nocount on
	if(@p is null and (select count(*) from BT) = 0)
	begin
		insert BT(OrgNode, number) values (hierarchyid::GetRoot(), @n)
	end
	else
	begin
		if(@p is null)
		begin
			set @p = cast('/' as hierarchyid)
		end
		declare @count int
		select @count = count(*) from BT where OrgNode.IsDescendantOf(@p) = 1 and OrgNode.GetLevel() = @p.GetLevel() + 1
		declare @value int
		select @value = number from BT where OrgNode = @p
		print @value
		declare @left hierarchyid, @right hierarchyid
		select top 1 @left = OrgNode from BT
			where OrgNode.IsDescendantOf(@p) = 1 and OrgNode.GetLevel() = @p.GetLevel() + 1 order by OrgNode asc
		select top 1 @right = OrgNode from BT
			where OrgNode.IsDescendantOf(@p) = 1 and OrgNode.GetLevel() = @p.GetLevel() + 1 order by OrgNode desc
		if(@value is null)
		begin
			update BT set number = @n where OrgNode = @p
		end
		else if(@n < @value)
		begin
			if(@count = 0)
			begin
				insert BT(OrgNode, number) values (@p.GetDescendant(NULL, NULL), @n)
				insert BT(OrgNode, number) values (
					@p.GetDescendant((select top 1 OrgNode from BT
						where OrgNode.IsDescendantOf(@p) = 1 and OrgNode.GetLevel() = @p.GetLevel() + 1), NULL), NULL
				)
			end
			else
			begin
				execute sp_dodajdodrzewa @n, @left
			end
		end
		else
		begin
			if(@count = 0)
			begin
				insert BT(OrgNode, number) values (@p.GetDescendant(NULL, NULL), NULL)
				insert BT(OrgNode, number) values (
					@p.GetDescendant((select top 1 OrgNode from BT
						where OrgNode.IsDescendantOf(@p) = 1 and OrgNode.GetLevel() = @p.GetLevel() + 1), NULL), @n
				)
			end
			else
			begin
				execute sp_dodajdodrzewa @n, @right
			end
		end
	end
end
go
exec sp_dodajdodrzewa 8
exec sp_dodajdodrzewa 3
exec sp_dodajdodrzewa 10
exec sp_dodajdodrzewa 1
exec sp_dodajdodrzewa 6
exec sp_dodajdodrzewa 4
exec sp_dodajdodrzewa 7
exec sp_dodajdodrzewa 14
exec sp_dodajdodrzewa 13
select OrgNode.ToString(), number from BT
/* zad. 5 */
declare @rvnode as hierarchyid, @rvold as hierarchyid, @rvnew as hierarchyid
select @rvnode = OrgNode from BT where number = 10
select @rvold = OrgNode from BT where number = 8
select @rvnew = OrgNode from BT where number = 13
select OrgNode.ToString() as Path, number from BT
select @rvnode.GetReparentedValue(@rvold, @rvnew).ToString()
update BT set OrgNode = @rvnode.GetReparentedValue(@rvold, @rvnew) where OrgNode = @rvnode
select OrgNode.ToString() as Path, number from BT
select @rvnew = OrgNode from BT where number = 6
select @rvnode.GetReparentedValue(@rvold, @rvnew).ToString()