Feature: AreaSummaryDashboard

@localskilldashboard
@regression
Scenario: AreaSummaryDashboard
	Given the user navigates to area summary dashboard page
	Then the user can load an area summary dashboard for Surrey
	And the user can load an area summary dashboard for Greater Manchester
	