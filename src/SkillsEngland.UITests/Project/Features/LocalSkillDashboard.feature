Feature: LocalSkillDashboard

@localskilldashboard
@regression
Scenario: LocalSkillDashboard
	Given the user navigates to local skills page
	Then the user can load a dashboard for Employment rate in Surrey
	And the user can load a dashboard for Employment rate in Buckinghamshire
	And the user can load a dashboard for Employment rate in Cheshire and Warrington