# Lab Guide: Integrate GenAI capabilities into your .NET apps with minimal code changes
## Exercise 2
**Description**

In this exercise 2, you will use Semantic Kernel to integrate with Azure OpenAI. Using Semantic Kernel can significantly enhance your AI application's capabilities by providing a unified interface, advanced orchestration, memory management, and robust error handling. It simplifies the integration process and ensures your application is scalable, secure, and resilient.

**Semantic Kernal client setup**
- Go to #region SemanticKernal and view the kernel client settings
- AddAzureOpenAIChatCompletion method is used to configure SK to use Azure OpenAI's Chat Completion service with AOAI endpoint, deploymnet name and default Azure credentials to use System Managed to connect to Azure OpenAI. 

**Prompt Execution Settings**
- Using PromptExecutionSettings parameters, you can control various aspects of how AI generates the model responses making it easier to tailor the AI's output to your specific needs.
    - Temperature: Controls the randomness of the generated text. Lower values make the output more deterministic, while higher values make it more creative.
    - MaxTokens: Specifies the maximum number of tokens (words or parts of words) in the generated response.
    - TopP: Controls the cumulative probability for token selection. It is used for nucleus sampling, where the model considers only the tokens with the highest probabilities that add up to TopP.

**Maintain State**
- Azure Open AI responses are stateless. Maintaining a chat history is crucial for providing context to the AI model, enabling it to generate more coherent and contextually relevant responses.
- Go to #region ChatHistory to view the prompts and responses are maintained 

**Azure Sign In**
- If you have already signed in to Azure, you can skip this step and move to deploy webapp
- Log into the provided Azure subscription in your environment using Azure CLI and on the Azure Portal using your credentials.
- Review the App Service Plan and the Azure Open AI service pre-provisioned in your subscription

### Deploy webapp to Azure App Service
- Right click on devshopDNC.csproj from Exercise 2 folder and select Open In Integrated Terminalaz login
 ![image](https://github.com/user-attachments/assets/2bbbf2a3-4373-474f-b71f-4eb02815ef76)
- To publish the web app, run the command in the opened terminal, run dotnet publish -c Release -o ./bin/Publish
- Right click on bin--> publish folder and select Deploy to webApp option
  
  ![image](https://github.com/user-attachments/assets/11a4d603-26fa-41f7-b5ae-bf5c3ac0f9db)
- Press Deploy
  
  <img width="254" alt="image" src="https://github.com/user-attachments/assets/1f66b1f2-7ecd-4b14-bb84-1ee3d0b8c680">
- Select the already existing webapp
  
  <img width="385" alt="image" src="https://github.com/user-attachments/assets/dbbabd67-6817-4eeb-8a4a-54b76e45a1ed">
  
### Run the webapp
- Once deployed, click on the Browse button on the portal by going to the App Service web app view to view the web app

  <img width="463" alt="image" src="https://github.com/user-attachments/assets/f8e4d56a-79ff-4f2d-9485-fe06ee69a8e3">

  
  <img width="796" alt="image" src="https://github.com/user-attachments/assets/810f108f-54e9-4047-812d-236efca80143">

### Enable Managed Identity

- The below step can be skipped if you completed Exercise 1

- System Identity has been already enabled for your web app. To view, search for Identity on Settings menu. Under System Assigned tab, the Status will be set to **ON**. 

<img width="796" alt="image" src="\images\Exercise-1-SMI.png">

- As a next step, on Azure Open AI Resource, web app  "Role Assignment" has been set as Cognitive Services OpenAI Contributor.

### Connect to Azure Open AI

Now, the website is up and running. Lets connect with Azure OpenAI to get the Chat with AI Assistant integrated to the web app 

Add these appsettings to App Service web app.

- The below step can be skipped if you completed Exercise 1

- Go to Azure Open AI on the portal and open it in Azure AI Studio
  
<img width="302" alt="image" src="https://github.com/user-attachments/assets/b5613558-b2d8-4390-b1b8-0a6718f3460c">

- Deploy the gpt-4o model by going to Deployments and select gpt-4o Chat completion model and click Confirm

<img width="368" alt="image" src="https://github.com/user-attachments/assets/8162929f-9d65-4d73-9eb2-0141645f1f39">

- Give the deployment name and select deployment type as "Global Standard"
 <img width="483" alt="image" src="https://github.com/user-attachments/assets/0de857de-f11e-4544-9aa3-3d64a07b9d08">

- Switch back to the App Service configuration blade. Add the environment variables DEPLOYMENT_NAME and ENDPOINT. For ENDPOINT value, use TargetUri and for DEPLOYMENT_NAME value, use deployment name retrieved from the above step 

<img width="796" alt="image" src="\images\Exercise-1-envvar.png">

### Chat with AI Assistant
- Go to Clothing tab and select a product. 
- Click on **Chat with AI Assistant** and give an user message like "tell me more about the formal blazer"
- You should receive a chat response from the OpenAI model 

  
### Monitor the webapp
To monitor your web app, you can leverage the LogStream option under Monitoring section on the webapp portal view.

<img width="771" alt="image" src="https://github.com/user-attachments/assets/239e41b0-6c2f-451f-9cab-853c516722fa">
