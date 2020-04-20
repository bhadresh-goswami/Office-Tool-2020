
--Candidate Details
--Course Candidate Manage
--Batch & Time Manage
--Report Manage

Create table StatusMaster
(
	StatusId int primary key identity,
	StatusTitle varchar(200) not null
)
Create table CandidateMaster
(
	CandidateId int primary key identity,
	CandidateName varchar(200) not null,
	CandidateEmailId varchar(200) unique not null,
	CandidateMobile varchar(20) unique not null,
	RefCandidateStatusTitle int not null foreign key references StatusMaster(StatusId)
)

Create table CourseMaster
(
	CourseId int primary key identity,
	CourseTitle varchar(200) not null unique,
	Duration int not null,
	Fees decimal not null,
	CourseContent varchar(200) not null,
	IsCourseEnabled bit not null
)

Create table CandidateCourseMaster(
	CCId int primary key identity,
	RefCandidateTitle int not null foreign key references CandidateMaster(CandidateId),
	RefCourseTitle int not null foreign key references CourseMaster(CourseId)
)

Create table BatchMaster
(
	BatchId int primary key identity,
	BatchName varchar(200) not null unique,
	RefExpertName int not null foreign key references ExpertMaster(ExpertId),
	StartTime time not null,
	EndTime time not null,
	StartDate date not null,
	CompletedDate date not null,
	BatchStatus int not null references StatusMaster(StatusId)
)
Create table CandidateBatchMaster
(
	CBId int primary key identity,
	RefCandidateCourseId int not null foreign key references CandidateCourseMaster(CCId),
	RefBatchTitle int not null foreign key references BatchMaster(BatchId)
)
Create table SessionMaster
(
	SessionId int primary key identity,
	SessionTitle varchar(500) not null,
	SessionTopic varchar(max) not null,
	SessionDate date not null,
	RefBatchTitle int not null foreign key references BatchMaster(BatchId),
	PresentCandidates varchar(max) not null
)
