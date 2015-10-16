<h2>Teamwork Project Assignment for the <a href="https://softuni.bg/courses/asp-net-mvc">ASP.NET MVC Course @ SoftUni</a></h2>
<p>This teamwork project assignment is designed to develop skills for creating dynamic data-driven Web applications using ASP.NET MVC and deployment in ? cloud environment like Azure and AppHarbor</p>

<h3>Project Description</h3>
<p>Design and implement a <strong>photo contest site</strong>.</p>

<p>The application should allow users to create and participate in contests for images. Each user can be both an organizer and a participant in different contests. Organizers should have a variety of options for how to carry out the contest.</p>

<p>We have laid out some application design suggestions but feel free to implement your own. Get creative!</p>

<p>A <strong>contest</strong> must have a title, description, reward strategy, voting strategy, participation strategy and deadline strategy. A contest also keeps track of all <strong>pictures</strong> submitted to it and their <strong>votes</strong>.</p>

<span>The reward strategy is one of the following:</span>
<ul>
    <li>Single winner – the owner of the picture with the most votes wins</li>
    <li>Top N prizes – list of prizes for the first N participants with the most votes</li>
</ul>

<span>The voting strategy is one of the following:</span>
<ul>
    <li>Open – everyone can vote</li>
    <li>Closed – only members of a committee (invited by the contest owner) can vote</li>
</ul>

<span>The participation strategy is one of the following:</span>
<ul>
    <li>Open – everyone can submit images</li>
    <li>Closed – participants are pre-selected by the contest owner</li>
</ul>

<span>The deadline strategy is one of the following:</span>
<ul>
    <li>By time – the contest closes automatically and may accept no new submissions at the given time</li>
    <li>By number of participants – the contest closes automatically after the selected number of participants have joined the contest 
No matter what the deadline strategy is, the contest owner can dismiss (stop, selecting no winners) and end (finalize, selecting a winner / winners according to the contest reward strategy) a contest.</li>
</ul>

<h3>General Requirements</h3>
<span>Your Web application should use the following technologies, frameworks and development techniques:</span>

<ul>
    <li>The application must be implemented using <strong>ASP.NET MVC framework</strong>.</li>
    <li>Use <strong>Visual Studio 2015 or 2013</strong> (Update 4 is recommended).</li>
    <li>
        Use <strong>Razor</strong> template engine for generating the UI.
        <ul>
             <li>Rendering with ASP.NET Web Forms is not allowed.</li>
             <li>Use <strong>sections</strong> and <strong>partial views</strong>.</li>
             <li>Use <strong>editor</strong> and <strong>display templates</strong>.</li>
        </ul>
    </li>
    <li>Use <strong>Microsoft SQL Server</strong> as database back-end.</li>
    <li>
        Use <strong>Entity Framework 6</strong> to access your database.
        <ul>
             <li>Obligatorily use Repository and Unit of Work patterns.</li>
        </ul>
    </li>
    <li>Use <strong>MVC Areas</strong> to separate different parts of your application (e.g. area for administration).</li>
    <li>
        Adapt the default <strong>ASP.NET MVC site template</strong> from Visual Studio 2013 or get another free theme.
         <ul>
             <li>Use responsive design based on <strong>Twitter Bootstrap</strong>.
         </ul>
    </li>
    <li>
        Use the standard <strong>ASP.NET Identity System</strong> for managing users and roles.
        <ul>
            <li>Your registered users should have at least one of these roles: <strong>user</strong> and <strong>administrator</strong>.</li>
        </ul>
    </li>
    <li>Use <strong>AJAX</strong> request to asynchronously load and display data somewhere in your application.</li>
    <li>Use <strong>SignalR</strong> communication somewhere in your application.</li>
    <li>Write <strong>unit tests</strong> for your logic, controllers, actions, helpers, etc.</li>
    <li>Implement <strong>error handling</strong> and <strong>data validation</strong> to avoid crashes when invalid data is entered (both <strong>client-side</strong> and <strong>server-side</strong>).</li>
    <li>Handle correctly the special HTML characters and tags like <strong>< br /></strong> and <strong>< script ></strong> (escape special characters).</li>
    <li>Use <strong>Ninject</strong> (or any other dependency injection container).</li>
    <li>Use <strong>Auto?apper</strong>.</li>
    <li><strong>Prevent</strong> from <strong>security vulnerabilities</strong> like SQL Injection, XSS, XSRF, parameter tampering, etc.</li>
    <li>Host the application in a <strong>cloud environment</strong>, e.g. in <strong>AppHarbor</strong> or <strong>Azure</strong>.</li>
    <li>Obligatorily use a file storage cloud API, e.g. Dropbox, Google Drive or other for storing the files.</li>
</ul>

<h3>Public Part</h3>
<p>The <strong>public part</strong> of your application should be <strong>visible without authentication</strong>. All users can see the active contests and the past contests, ordered by date (from the soonest to the earliest).

<p><strong>Design suggestion:</strong></p>
<p>All users can see all the active contests on the home page. Upon clicking a contest, a user can go to the contest details page (also public) and vote for the winners (in case the voting strategy for the contest permits). There can also be a "Past Contests" page where everyone can see the ended / dismissed contests <strong>without</strong> their winners.</p>

<h3>Private Part (User Part)</h3>

<p><strong>Registered users</strong> should be able to <strong>login</strong>. This can happen with a local account, and via Facebook and Google. You may also link the application to other external login providers.</p>

<span>Registered users can:</span>
<ul>
	<li>Manage contests:</li>
	<ul>
		<li><strong>Create</strong> a contest</li>
		<li><strong>Update</strong> a contest (if they are the owners)</li>
		<li><strong>Dismiss</strong> a contest - stop the contest and select no winners</li>
		<li><strong>Finalize</strong> a contest - initialize the end of the contest and choose a winner / winners in accordance with its voting strategy</li>
	</ul>
	<li>Participate in contests:</li>
	<ul>
		<li><strong>Register for a contest</strong> - a user can enter an open contest freely, or be invited to a closed contest by its owner. It’s only after being invited that the user can participate in a closed contest</li>
		<li><strong>Upload image</strong> as an entry for a specific contest. A user may submit more than one image for a contest</li>
		<li><strong>Dismiss</strong> a contest - stop the contest and select no winners</li>
		<li><strong>View contest results </strong> - see the winner(s) of a contest</li>
	</ul>
</ul>

<p><strong>Design suggestion:</strong></p>
<p>Once a user is logged in, display a menu (or some links) to the user’s contests. Create a page listing all the user’s contests and provide options for creating new contests and editing currently active contests. For example, you can display all contests and have an "Edit" button. When the user presses it, a page with the contest parameters opens and the user is free to edit the contest title, details and deadlines. There can also be a "Dismiss" and a "Finalize" button, along with the "Edit" button.
On the home page, the registered users can also see the active contests, but now they should have the option to participate in one. If an active contest is closed and the user is not invited, they should not be able to participate in it.
</p>

<h3>Administration Part</h3>
<p><strong>An administrator</strong> should have access to all contests, as if he / she is the contest creator. He / She can also manage other users’ profiles (excluding their own profile, and excluding usernames). The admins can also delete pictures from contests if they think they are inappropriate.</p>

<p>A user can be an administrator and still be able to create and take part in contests (i. e., admins have all rights that non-admins have, plus some more). An administrator <strong>cannot edit votes</strong> for pictures.</p>

<p><strong>Design suggestion:</strong></p>
<p>Reuse the logic (and code, if possible) for the user’s contests section. This time, display all contests and allow editing them. For a specific contest, display a "Delete Picture" button next to every picture. Be sure to ask the administrator whether they really want to delete that picture.</p>

<p>For the users, you may display them in a grid (optionally, with the ability to search by username) and allow editing their personal details.</p>

<h3>Additional Requirements</h3>
<ul>
	<li>
		Follow the best practices for OO design and <strong>high-quality code</strong> for the Web application:
		<ul>
			<li>Use data encapsulation</li>
			<li>Use exception handling properly.</li>
			<li>Use inheritance, abstraction and polymorphism properly.</li>
			<li>Follow the principles of strong cohesion and loose coupling.</li>
			<li>Correctly format and structure your code, name your identifiers and make the code readable.</li>
		</ul>
	</li>
	<li>Well looking user interface (UI).</li>
	<li>Good usability (easy to use UI).</li>
	<li>Supporting of all modern Web browsers.</li>
	<li>Use caching where appropriate.</li>
	<li>
		Use a <strong>source control system</strong> by choice, e.g. Git, SVN, GitHub, CodePlex.
		<ul>
			<li>Submit a link to your public source code repository.</li>
		</ul>
	</li>
</ul>
