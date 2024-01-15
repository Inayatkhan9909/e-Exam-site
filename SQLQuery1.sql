Create table Exam_details(Exam_id int primary key identity(3424,323),
Exam_name varchar(100) not null,Exam_date varchar(100) not null,
Exam_description varchar(1000) not null)

insert into Exam_details(Exam_name,Exam_date,Exam_description) values('NEET','24 january 2024',
'This is a mock test to prepare you for neet examination')
insert into Exam_details(Exam_name,Exam_date,Exam_description) values('Python','29 january 2024',
'This is a mock test to prepare you for Python examination')

select * from Exam_details