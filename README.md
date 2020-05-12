# ac-proger

Todo:
	Posts:
		1. Add Page(Action) CreatePost To PostController:
			a. Action can be reached only by authorized, not-banned users (For other - show login page).
			b. Name, Text, Image And Category is required.
			c. Posts must be approved after creating, so all created posts must be saved in Posts_Staging table.
		2. Add link 'Create Post' to CreatePost(Action) at GetAllHeaderPosts pages.
		3. Add Page(Action) ViewStagingPost To PostController:
			a. Required input - StagingPostID 
			b. can be reached only by users with role moderrator or Admin.
			c. show StagingPost on this page.
			d. Add button Approve at the end of post.
			e. After Clicking button 'Approve' Copy post From table Posts_Staging to table Posts and delete record about this post from table Posts_Staging.
		4. Add Page(Action) GetAllStagingPosts To PostController:
			a. this page can be reached only by users with role - moderator or Admin.
			b. Add grid with all not approved posts.
			c. Add button 'View Post' for selected post.
			d. After Clicking button 'View Post' - Redirect user to 'ViewStagingPost' in 'PostController' for selected StagingPost.
			
			
	Side-Menu:
		1. Add table Parent-Categories:
			a. PK_Parent_Category 					INT PRIMARY KEY IDENTITY(1,1)
			b. Name									VARCHAR(MAX)
			c. Description 							VARCHAR(MAX)
		2. UPDATE table Categories:
			a. Add Column FK_Parent_Category		INT FOREIGN KEY Parent-Categories(PK_Parent_Category)
		3. Create Side-menu:
			a. Menu must Load Categories from DB;
			b. menu must be something like this:
			
			-Parent-Category1[Opened]
			----Category1
			----Category2
			-Parent-Category2[Opened]
			----Category3
			----Category4
			-Parent-Category3[Closed]
			
		4. Create Some Basc design for it.

		
	Header-menu:
		1. Think About What we can do with it.
			There are Some ideas:
				[bad idea]a.Header-menu can be just like a side-menu (load parent-categories and categories from db)
				[better idea]b. Change Header-menu content. So it can be like:
				
				Posts;
				--View All Posts;
				--View My Posts;
				--Create New post
				My Account
				-- Edit Account;
				-- Delete Account;
				-- Show my Stats
				Support
				-- Create ticket;
				-- show my tickets;
				Log-Out
				
				
	EmailBusiness:
		1. Test Email Sending !.
		2. Write What to do here.
				
	Registration:
		1. Add Email Confirmation after clicking register on registration page:
			a. Add Table Confirmation_Email:
				PK_Confirmation_Email  				INT PRIMARY KEY IDENTITY(1,1)
				FK_User 							INT FOREIGN KEY User(user_id)
				Confirmation_Code					VARCHAR(MAX)
			b. Update Table Users:
				Add Column EmailConfirmation				INT 
			c. Set EmailConfirmation = false In table users for user
			d. Generate random number('42578429'), Insert record in table Confirmation_Email
				KEY  User_ID  GENERATED_NUMBER
			e. Send Generated Number to the user email
			f. if user wrote number equal to Confirmation_Code at table Confirmation_Email than
				Set EmailConfirmation = true In table users for user
				Delete Record About User at table Confirmation_Email
		2.	Change Log-in logic:
			a. Check if user is confirmated(At Users table check if EmailConfirmation == true)
			b. If User Is not Confirmated redirect him to confirmation page.
			
			
	Side-Chat:
		Think about it.
		1. Add table Chat_Messages
			PK_Message 								INT PRIMARY KEY IDENTITY(1,1)
			Message									VARCHAR(MAX)
			FK_User									INT FOREIGN KEY Users(user_id)
		2. Add API Controller ChatMessageController:
			string GetLastMessage();
			string GetLastMessageFromUser(int user_id);
			string[] GetLastMessages(int how_many_messages_skip, int how_many_messages_get);
		3. Create Page And Design For Chat
		4. Add JS Code for API Controller
		
	Users:
		1. Add rating for posts
			Update table Posts
				Add Column Rating					INT 
		2. Add rating for users
			Add Column Rating
			
		3. Add 10 stars for ViewPost page
			agter clicking on star - set rating for post
			and than get rating for every post from this user and set him rating:
			sum_of_all_posts_rating/num_of_all_Posts
			
		4. Show User Rating like stars At ViewUserStats page 