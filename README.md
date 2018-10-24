<h2 id="bkmrk-hi%2C" class="align-center">CosmosDBGraphRestAPI</h2>
<li>Estimated time to complete the how-to : 10 minutes</li>
<li>Level : 200</li>
<p id="bkmrk-cosmosdb-supports-a-">CosmosDB supports a graph database that supports Grmlin traversal language.</p>
<p id="bkmrk-unfortunately%2C-it-do">Unfortunately, it does not support a REST API, and this is disappointing for some people, that wants to use CosmosDB Graph in different language than the available APIs (Java, c#...).</p>
<p id="bkmrk-fortunately%2C-using-o">Fortunately, using only an Azure Function, you can build your own or a public CosmosDB graph Rest API, that you can use in real life.</p>
<h2 id="bkmrk-1--how-%3F">1- How ?</h2>
<ul id="bkmrk-create-an-azure-func">
<li>Create an Azure Function (C# in my case)</li>
<li>Create a generic request that sends a query to CosmosDB using the c# library</li>
<li>Expose a HTTP endpoint for your function</li>
<li>Request the HTTP endpoint, catch the header content (than contains the gremlin request), and send it back to Cosmos using the c# request</li>
</ul>
<p id="bkmrk---%3E-very-easy">--&gt; Very Easy</p>
<div drawio-diagram="113"><img id="image-e0641fb58bb48" src="https://wiki.internal.cloudcase.io/uploads/images/drawio/2018-10-Oct/iL6Gd9DXCCWbYjJ8-Drawing-Samir-Farhat-1540359339.png" /></div>
<h2 id="bkmrk-2--requirements">2- Requirements</h2>
<ul id="bkmrk-visual-studio-2017--">
<li>Visual Studio 2017&nbsp;</li>
<li>Git bash (Optional)</li>
<li>An Internet connection</li>
<li>An Azure Subscription to deploy an Azure Function and a CosmosDB account/DB</li>
</ul>
<h2 id="bkmrk-3--steps">3- Steps</h2>
<h3 id="bkmrk-3.1--cloning-the-rep">3.1- Cloning the repository</h3>
<p id="bkmrk-1--create-a-new-work">1- Create a new work folder on your work machine</p>
<p id="bkmrk-2--download-the-bits">2- Download the bits from&nbsp;git clone <a href="https://github.com/SamirFarhat/CosmosDBGraphRestAPI.git">https://github.com/SamirFarhat/CosmosDBGraphRestAPI.git </a></p>
<p id="bkmrk-you-can-use-git-bash">You can use Git bash to clone the repo :&nbsp; git clone https://github.com/SamirFarhat/CosmosDBGraphRestAPI.git</p>
<p id="bkmrk-"><img id="image-511d876291393" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/ohGsTeJybXMiMxNc-image-1540354960832.png" width="239" height="336" /></p>
<p id="bkmrk--0"><img id="image-065c33f7817a3" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/QBCQosgUUSqBf4BV-image-1540354968913.png" width="322" height="188" /></p>
<p id="bkmrk--1"><img id="image-51a0bae73e48c" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/Ewjztyq0egmiyYRM-image-1540355008188.png" width="356" height="102" /></p>
<p id="bkmrk-the-repo-files-will-">The repo files will be cloned to your folder</p>
<p id="bkmrk--2"><img id="image-5a93d63a5ad85" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/ImsDd14ehkjl6DgI-image-1540355153988.png" width="396" height="118" /></p>
<h3 id="bkmrk-3.2--changing-the-co">3.2- Prerequisites</h3>
<h4 id="bkmrk-3.3.1--prepare-the-a">3.3.1- Prepare the Azure Resources</h4>
<p id="bkmrk-use-any-text-editor%2C">Before continuing you have to create:</p>
<p id="bkmrk---an-azure-function-">-<strong> An Azure Function</strong> : I recommend using a V2 Function</p>
<p id="bkmrk--3"><img id="image-e729b30db3a21" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/UbFstQ909UOLThfR-image-1540355661746.png" width="161" height="289" /></p>
<p id="bkmrk-%C2%A0-0">-<strong> A CosmosDB account (Grah API)</strong></p>
<p id="bkmrk-%C2%A0-1">&nbsp;</p>
<h4 id="bkmrk-3.2.2--installing-de">3.2.2- Installing dependencies&nbsp;and prerequisites&nbsp;in Visual Studio</h4>
<ul id="bkmrk-open-visual-studio-g">
<li>Open Visual Studio</li>
<li>Go to <strong>Edit</strong> --&gt; <strong>Get Tools and Features</strong></li>
</ul>
<p id="bkmrk--4"><img id="image-97d33bc4e3991" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/w2lYCZJysBxV00Q5-image-1540356362058.png" width="267" height="58" /></p>
<ul id="bkmrk-check-that-the-azure">
<li>Check that the Azure Development is installed, or install it if not</li>
</ul>
<p id="bkmrk--5"><img id="image-1e3ceaba20364" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/ZsBByetlaopPOrIM-image-1540356371800.png" width="404" height="186" /></p>
<ul id="bkmrk-go-to-edit---%3E-exten">
<li>Go to <strong>Edit</strong> --&gt; <strong>Extensions and Updates</strong></li>
</ul>
<p id="bkmrk--6"><img id="image-f9b4de7fcebce" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/I3LgGrF34K6v2fKA-image-1540356101762.png" width="382" height="83" /></p>
<ul id="bkmrk-install-or-update-th">
<li>Install or update the <strong>Azure Functions and web Jobs tools</strong></li>
</ul>
<p id="bkmrk--7"><img id="image-3c45ece6684b8" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/rqmUiyekJNzpdbjX-image-1540356069900.png" width="401" height="165" /></p>
<h3 id="bkmrk-3.3--edit-some-confi">3.3- Edit some configuration</h3>
<h4 id="bkmrk-3.3.1--installing-mi">3.3.1- Installing missing packages</h4>
<p id="bkmrk-if-you-notice-that-y">If you notice that you have missing packages (the using xxxxx is underlined), then do the following:</p>
<ul id="bkmrk-go-to-other-windows-">
<li>Go to Other Windows --&gt; Package Manager Console</li>
</ul>
<p id="bkmrk--8"><img id="image-58763a7f7bf0d" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/Qh3j9Y3VfhRnmpAb-image-1540356611142.png" width="317" height="189" /></p>
<ul id="bkmrk-example-%3A-if%C2%A0using-">
<li>Example : if&nbsp;using Microsoft.Azure.Graphs then google it and add 'nuget'<img id="image-0c14a7ec38ac2" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/sdqgWo3OyaJszh2l-image-1540356752601.png" width="308" height="137" /></li>
<li>Copy the command and run it on Package Manager</li>
</ul>
<p id="bkmrk--9"><img id="image-500f8f2749eeb" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/saV2wNghc2KfayfL-image-1540356570272.png" width="334" height="39" /></p>
<p id="bkmrk-%C2%A0-2">&nbsp;</p>
<h4 id="bkmrk-3.3.1--updating-cust">3.3.1- Updating custom settings</h4>
<ul id="bkmrk-open-the-solution-%28s">
<li>Open the solution (sln file) in Visual Studio</li>
</ul>
<p id="bkmrk--10"><img id="image-e613d0cf4483c" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/MaLZnGv1wlZBqpW5-image-1540355776581.png" width="181" height="85" /></p>
<ul id="bkmrk-open-the-solution-%28s-0">
<li>Open the solution (sln file) in Visual Studio</li>
</ul>
<p id="bkmrk-%C2%A0-3">Change the following information :</p>
<ul id="bkmrk-grahdb-%3A-your-db-nam">
<li>GrahDB : Your DB Name</li>
<li>GraphName : The name of the graph</li>
<li>OfferTroughput : The graph RU</li>
</ul>
<p id="bkmrk--11"><img id="image-d39b02dd4a2a" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/pDflg6pygLUnyNUH-image-1540355857739.png" width="340" height="121" /></p>
<p id="bkmrk-%C2%A0-4">--&gt; Save</p>
<p id="bkmrk-nb-%3A-if-the-db-and-c"><strong>NB : If the DB and collection are not found during the function execution they will be created.</strong></p>
<h3 id="bkmrk-3.4--test-the-functi">3.4- Test the Function</h3>
<h4 id="bkmrk-3.4.1--publish-the-f">3.4.1- Publish the Function</h4>
<ul id="bkmrk-right-click-the-proj">
<li>Right click the Project Name, and click <strong>Publish</strong></li>
</ul>
<p id="bkmrk--12"><img id="image-48eab7277dd16" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/wFhgpFQsIJKN5eC1-image-1540357649958.png" width="212" height="255" /></p>
<ul id="bkmrk-my-function-app-alre">
<li id="bkmrk-%C2%A0-6">My Function App already exist, so i Select and Existing app. Select the Run from ZIP so that your code be read-only on the Azure Function, and no one can change it. <span style="color: #ff0000;">Note that the Function App will be marked as Read-Only and that you can only publish apps from external sources (Not from the Portal anymore)</span></li>
</ul>
<p id="bkmrk--13"><img id="image-6d376ecc8b649" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/2zScCHez2CtxidDq-image-1540357661214.png" width="296" height="222" /></p>
<p id="bkmrk--14"><img id="image-bc3e69440b211" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/QiGOZs8w2e4G2mOM-image-1540357685553.png" width="296" height="156" /></p>
<p id="bkmrk-%C2%A0-9">--&gt; The Function should be now be published</p>
<h4 id="bkmrk-3.4.2--add-the-missi">3.4.2- Add the missing App settings</h4>
<ul id="bkmrk-go-the-function-on-a">
<li>Go the Function on Azure Portal, and click <strong>Application Settings</strong></li>
</ul>
<p id="bkmrk--15"><img id="image-a3e1d90eaf1" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/Gz3B8muZEIJzNCWF-image-1540357989842.png" width="285" height="130" /></p>
<ul id="bkmrk-add-two-new-settings">
<li>Add two new settings:
<ul>
<li><strong>AuthKey</strong>: Can be found under CosmosDB Account --&gt; Keys</li>
<li><strong>Endpoint</strong> :&nbsp;Can be found under CosmosDB Account --&gt; Keys
<p><img id="image-be5d5c2b56b94" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/iVf0iNdeU95XFywX-image-1540358161126.png" width="227" height="154" /></p>
</li>
</ul>
</li>
</ul>
<p id="bkmrk--16"><img id="image-01bde9ef6deea" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/mnYuCN591PIQkoII-image-1540357953503.png" width="236" height="175" /></p>
<p id="bkmrk-%C2%A0-11">--&gt; <strong>Save</strong></p>
<p id="bkmrk-%C2%A0-12">&nbsp;</p>
<h4 id="bkmrk-3.4.3--make-you-firs">3.4.3- Make you first test</h4>
<ul id="bkmrk-get-the-function-url">
<li id="bkmrk-%C2%A0-14">Get the function URL</li>
</ul>
<p id="bkmrk--17"><img id="image-66b826025ebde" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/J5kYzMgBJVqeHqVC-image-1540358468936.png" width="341" height="98" /></p>
<p id="bkmrk-%C2%A0-15">&nbsp;</p>
<ul id="bkmrk-make-a-rest-call-usi">
<li id="bkmrk-%C2%A0-16">Make a Rest call using Postman for example (Download from here)
<ul>
<li>Post</li>
<li>The Header must include the gremlin query on <strong>Gremlinquery</strong></li>
</ul>
</li>
</ul>
<p id="bkmrk--18"><img id="image-810c776efae68" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/bVRVJucMDrYTCmMR-image-1540358551410.png" width="380" height="185" /></p>
<p id="bkmrk-%C2%A0-17">And it works !</p>
<p id="bkmrk-i-will-test-posting-">I will test posting the following query to create a new user called : <strong>MorningBlog</strong></p>
<p id="bkmrk-g.addv%28%27user%27%29.prope"><code><strong>g.AddV('user').Property('name', 'morningblog')</strong></code></p>
<p id="bkmrk--19"><img id="image-0dc43ef85f73b" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/FO2TOsUAOdyUnxwp-image-1540358727563.png" width="338" height="191" /></p>
<p id="bkmrk-%C2%A0-19">And here is it using the Explorer</p>
<p id="bkmrk--20"><img id="image-ea6ed630ead7e" src="https://wiki.internal.cloudcase.io/uploads/images/gallery/2018-10-Oct/scaled-840-0/D7v7AiEmQuHrpTf8-image-1540358790124.png" width="352" height="207" /></p>
<p id="bkmrk-%C2%A0-5">&nbsp;</p>
