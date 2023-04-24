# locals {
#   front_door_name = "gsej-frontdoor"
#   front_door_frontend_endpoint_name = "frontEndEndpoint"
#   front_door_load_balancing_settings_name = "loadBalancingSettings"
#   front_door_health_probe_settings_name = "healthProbeSettings"
#   front_door_routing_rule_name = "routingRule"
#   front_door_backend_pool_name = "backendPool"
# }

# # variable "backend_address" {
# #   type = string
# #   default = azurerm_windows_web_app.myapp.default_site_hostname
# # }

# resource "azurerm_frontdoor" "my_front_door" {
#   name                = local.front_door_name
#   resource_group_name = azurerm_resource_group.example.name

#   frontend_endpoint {
#     name      = local.front_door_frontend_endpoint_name
#     host_name = "${local.front_door_name}.azurefd.net"
#     session_affinity_enabled = false
#   }

#   backend_pool_load_balancing {
#     name = local.front_door_load_balancing_settings_name
#     sample_size = 4
#     successful_samples_required = 2
#   }

#   backend_pool_health_probe {
#     name = local.front_door_health_probe_settings_name
#     path = "/"
#     protocol = "Http"
#     interval_in_seconds = 120
#   }

#   backend_pool {
#     name = local.front_door_backend_pool_name
#     backend {
# 	host_header = azurerm_windows_web_app.myapp.default_hostname
# 	address = azurerm_windows_web_app.myapp.default_hostname
# #      host_header = var.backend_address
# #      address     = var.backend_address
#       http_port   = 80
#       https_port  = 443
#       weight      = 50
#       priority    = 1
#     }

#     load_balancing_name = local.front_door_load_balancing_settings_name
#     health_probe_name   = local.front_door_health_probe_settings_name
#   }

#   routing_rule {
#     name               = local.front_door_routing_rule_name
#     accepted_protocols = ["Http", "Https"]
#     patterns_to_match  = ["/*"]
#     frontend_endpoints = [local.front_door_frontend_endpoint_name]
#     forwarding_configuration {
#       forwarding_protocol = "MatchRequest"
#       backend_pool_name   = local.front_door_backend_pool_name
#     }
#   }
# }