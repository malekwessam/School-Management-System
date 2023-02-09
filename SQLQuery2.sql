create table CourseLevel
(
CourseLevelId int  identity(1,1) primary key,
level  nvarchar(50)  
)
insert into CourseLevel values ('Begginer'),('InterMediate'),('Advanced'),('Insane')
 select * from CourseLevel

 alter table Course 
 add Level2 int; 

 alter table Course 
 add foreign key (Level2) References CourseLevel(CourseLevelId)