
  alter table  [MaterialPrice] add UnitStartPrice decimal(18,2) 
  update [MaterialPrice] set UnitStartPrice=0
  alter table [MaterialPrice] alter column UnitStartPrice decimal(18,2)  not null


