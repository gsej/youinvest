terraform {
  required_version = ">=1.4.5"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>3.53.0"
    }
    random = {
      source  = "hashicorp/random"
      version = "~>3.0"
    }
  }

}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}

resource "azurerm_resource_group" "rg" {
  name     = var.resource_group_name
  location = var.location
}


# resource "azurerm_windows_web_app" "myapp" {
#   name                = "gsej-myapp"
#   resource_group_name = azurerm_resource_group.example.name
#   location            = azurerm_service_plan.myplan.location

#   service_plan_id     = azurerm_service_plan.myplan.id

#   https_only = true

#   site_config {
#     use_32_bit_worker = false
#   }
# }


# resource "random_id" "front_door_name" {
#   byte_length = 8
# }
