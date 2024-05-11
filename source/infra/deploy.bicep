//BUILD: az bicep build --file deploy.bicep --stdout
//VALIDATE: az deployment sub validate --subscription "subscription" --location WestEurope --template-file api-deploy.bicep --parameters env=dev
//PREVIEW: az deployment sub what-if --subscription "subscription" --location WestEurope --template-file api-deploy.bicep --parameters env=dev
//DEPLOY: az deployment sub create --subscription "subscription" --location WestEurope --template-file api-deploy.bicep --parameters env=dev

targetScope = 'subscription'

@allowed([
  'dev'
  'test'
  'prod'
])
param env string = 'dev'
@allowed([
  'weu'
])
param locationKey string = 'weu'
@allowed([
  'freethings'
])
param resourceKey string = 'freethings'
param resourceSuffix string = '01'
param location string = deployment().location

var environmentConfigMap = {
  dev: {
      prefix: 'd-${locationKey}-${resourceKey}'
  }
  test: {
      prefix: 't-${locationKey}-${resourceKey}'
  }
  prod: {
      prefix: 'p-${locationKey}-${resourceKey}'
  }
}

// Create a resource group
resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: '${environmentConfigMap[env].prefix}-rg${resourceSuffix}'
  location: location
}

// Create an Azure Container App, with .NET 8
resource containerApp 'Microsoft.ContainerApp/containerApps@2021-08-01-preview' = {
  name: '${environmentConfigMap[env].prefix}-ca${resourceSuffix}'
  location: location
  properties: {
    containers: [
      {
        name: 'app'
        properties: {
          image: 'mcr.microsoft.com/dotnet/aspnet:5.0'
          resources: {
            requests: {
              cpu: '1.0'
              memoryInGB: '1.5'
            }
          }
        }
      }
    ]
  }
  dependsOn: [
    resourceGroup
  ]
}

