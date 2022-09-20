Feature: Userrestrictions

Scenario Outline: Set User Restrictions
	Given I login to confluence cloud
	And I navigate to the page to configure permissions
	When I apply "<Restriction>"
	Then I see "<Expected Result>"

	Examples: 
	| Restriction                           | Expected Result                                                                        |
	| Anyone can view and edit              | Anyone can view and edit                                                               |
	| Anyone can view, only some can edit   | Anyone can view and only users with permission can edit                                |
	| Only specific people can view or edit | Only users with permission to view can view and users with permission to edit can edit |