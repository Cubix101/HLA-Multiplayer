local myId
local file
local offScreenPos
local localPlayer
local serverInterface = require "ServerInterface"

--spawn a new player
function SpawnPlayer(p_id)
    localPlayer = Entities:GetLocalPlayer()
    PlayerSpawner = Entities:FindByName(nil, "p_spawn_relay")
    EntFireByHandle(localPlayer,PlayerSpawner,"Trigger")
end

function Start()
    --offScreenPos = EntityGroup[1]:GetOrigin()
    --localPlayer = Entities:GetLocalPlayer()
    --1. what is this 2. this references a file specifically on your computer so this will only work for people named Peter with this folder in their documents
    file = LoadKeyValues("C:/temp-client/GameInfo.txt")
    -- this finds the local player and then informs the gamemanager of the local id.
    for i=1, file["playerCount"] do
        local playerData = LoadKeyValues("C:/Users/Peter/Documents/Half-Life Alyx Multiplayer/Build/Client/temp-client/"..tostring(i)..".txt")
        if (playerData["IsLocal"] == "True") then
            print("Found my id")
            myId = i
        end
    end
    SpawnPlayer(myId)
end


function Update()
    --This is really fucking ugly and error prone. Remind me to make a function to generate an args string from a string table.
    local args = "1"..","..tostring(localPlayer:GetAbsOrigin().x)..","..tostring(localPlayer:GetAbsOrigin().y)..","..tostring(localPlayer:GetAbsOrigin().z)..","..tostring(localPlayer:GetAngles().x)..","..tostring(localPlayer:GetAngles().y)..","..tostring(localPlayer:GetAngles().z)
    serverInterface.SendData(0, args)
end

