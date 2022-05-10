game 'rdr3'
fx_version 'adamant'
rdr3_warning 'I acknowledge that this is a prerelease build of RedM, and I am aware my resources *will* become incompatible once RedM ships.'

client_scripts {
  'vorpclothingstore_cl.net.dll'
}

server_scripts {
  'vorpclothingstore_sv.net.dll'
}

files {
  'MenuAPI.dll',
  'Newtonsoft.Json.dll'
}

--dont touch
version '1.0'
vorp_checker 'yes'
vorp_name '^4Resource version Check^3'
vorp_github 'https://github.com/VORPCORE/VORP-ClothingStore'
