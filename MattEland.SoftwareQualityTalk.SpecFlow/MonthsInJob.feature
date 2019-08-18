Feature: MonthsInJob

The resume should be scored with a point for each month in the job

@scoring
Scenario: Months in Job
	Given A resume
	When the candidate has held a job for 14 months
	Then their score should be 14


