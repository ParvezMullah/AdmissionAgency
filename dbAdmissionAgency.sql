SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblFieldOfInterest]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblFieldOfInterest](
	[fieldName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblFieldOfInterest] PRIMARY KEY CLUSTERED 
(
	[fieldName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAgent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblAgent](
	[emailId] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[firstName] [nvarchar](50) NULL,
	[middleName] [nvarchar](50) NULL,
	[lastName] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[licenseNo] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NULL,
	[highestEduaction] [nvarchar](50) NULL,
	[mailingAddress] [nvarchar](500) NULL,
	[country] [nvarchar](50) NULL,
	[businessCard] [varbinary](max) NULL,
	[Resume] [varbinary](max) NULL,
	[Experience] [nvarchar](50) NULL,
	[location] [nvarchar](50) NULL,
	[CTC] [nvarchar](50) NULL,
	[Priority] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblAgent] PRIMARY KEY CLUSTERED 
(
	[emailId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStudent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblStudent](
	[emailId] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[firstName] [nvarchar](50) NOT NULL,
	[middleName] [nvarchar](50) NULL,
	[formerLastName] [nvarchar](50) NULL,
	[preferredFirstName] [nvarchar](50) NULL,
	[familyName] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[Dob] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Nationality] [nvarchar](50) NULL,
	[immigrationStatus] [nvarchar](50) NULL,
	[firstSpokenLanguage] [nvarchar](50) NULL,
	[currentAddress] [nvarchar](500) NULL,
	[permanentAddress] [nvarchar](500) NULL,
	[nameOfHighSchool] [nvarchar](50) NULL,
	[dateOfSchoolLeaving] [nvarchar](50) NULL,
	[nameOfPostSecondary] [nvarchar](50) NULL,
	[highestEducaion] [nvarchar](50) NULL,
	[fieldOfStudy] [nvarchar](50) NULL,
	[lastAttendedPostSecondary] [nvarchar](50) NULL,
	[IELTSResult] [varbinary](max) NULL,
	[termToBegin] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblStudent] PRIMARY KEY CLUSTERED 
(
	[emailId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblProgramOfInterest]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblProgramOfInterest](
	[fieldName] [nvarchar](50) NULL,
	[ProgramName] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSuccessRate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSuccessRate](
	[emailId] [nvarchar](50) NOT NULL,
	[universityName] [nvarchar](50) NULL,
	[successCount] [decimal](18, 0) NULL,
	[totalAttempt] [decimal](18, 0) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUniversities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUniversities](
	[emailId] [nvarchar](50) NOT NULL,
	[University] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblReferences]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblReferences](
	[emailId] [nvarchar](50) NOT NULL,
	[Ref] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblKeySkills]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblKeySkills](
	[emailId] [nvarchar](50) NOT NULL,
	[keySkills] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblApplications]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblApplications](
	[emailId] [nvarchar](50) NOT NULL,
	[fieldOfInterest] [nvarchar](50) NULL,
	[programOfInterest] [nvarchar](50) NULL,
	[University] [nvarchar](50) NULL,
	[Agent] [nvarchar](50) NULL,
	[applicationStatus] [nvarchar](50) NULL,
	[visaStatus] [nvarchar](50) NULL,
	[scheduledVisaAppointment] [nvarchar](50) NULL,
	[expectedVisaArrival] [nvarchar](50) NULL,
	[Fees] [int] NULL,
	[feesPaid] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDocuments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblDocuments](
	[emailId] [nvarchar](50) NOT NULL,
	[Passport] [varbinary](max) NULL,
	[Transcript] [varbinary](max) NULL,
	[birthCertificate] [varbinary](max) NULL,
	[visaStatus] [nvarchar](max) NULL,
	[docOfFamilyWealth] [varbinary](max) NULL,
	[IELTSResults] [varbinary](max) NULL,
	[Resume] [varbinary](max) NULL
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Program_Fild]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblProgramOfInterest]'))
ALTER TABLE [dbo].[tblProgramOfInterest]  WITH CHECK ADD  CONSTRAINT [FK_Program_Fild] FOREIGN KEY([fieldName])
REFERENCES [dbo].[tblFieldOfInterest] ([fieldName])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblSuccessRate_tblAgent]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSuccessRate]'))
ALTER TABLE [dbo].[tblSuccessRate]  WITH CHECK ADD  CONSTRAINT [FK_tblSuccessRate_tblAgent] FOREIGN KEY([emailId])
REFERENCES [dbo].[tblAgent] ([emailId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUniversity_tblAgent]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUniversities]'))
ALTER TABLE [dbo].[tblUniversities]  WITH CHECK ADD  CONSTRAINT [FK_tblUniversity_tblAgent] FOREIGN KEY([emailId])
REFERENCES [dbo].[tblAgent] ([emailId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblReferences_tblAgent]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblReferences]'))
ALTER TABLE [dbo].[tblReferences]  WITH CHECK ADD  CONSTRAINT [FK_tblReferences_tblAgent] FOREIGN KEY([emailId])
REFERENCES [dbo].[tblAgent] ([emailId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblKeySkills_tblAgent]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblKeySkills]'))
ALTER TABLE [dbo].[tblKeySkills]  WITH CHECK ADD  CONSTRAINT [FK_tblKeySkills_tblAgent] FOREIGN KEY([emailId])
REFERENCES [dbo].[tblAgent] ([emailId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblApplications_tblStudent]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblApplications]'))
ALTER TABLE [dbo].[tblApplications]  WITH CHECK ADD  CONSTRAINT [FK_tblApplications_tblStudent] FOREIGN KEY([emailId])
REFERENCES [dbo].[tblStudent] ([emailId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblDocuments_tblStudent]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblDocuments]'))
ALTER TABLE [dbo].[tblDocuments]  WITH CHECK ADD  CONSTRAINT [FK_tblDocuments_tblStudent] FOREIGN KEY([emailId])
REFERENCES [dbo].[tblStudent] ([emailId])
