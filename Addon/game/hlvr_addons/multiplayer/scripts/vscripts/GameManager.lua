local myId
local file
local offScreenPos
local localPlayer

--spawn a new player
function SpawnPlayer(p_id)
    EntFire("p_spawn_relay","Trigger",0)
end

end
function Start()
    offScreenPos = EntityGroup[1]:GetOrigin()
    localPlayer = Entities:GetLocalPlayer()
    --1. what is this 2. this references a file specifically on your computer so this will only work for people named Peter with this folder in their documents
    file = LoadKeyValues("C:/Users/Peter/Documents/Half-Life Alyx Multiplayer/Build/Client/temp-client/GameInfo.txt")
    -- this gives you an id (unique to your player)
    for i=1, file["playerCount"] do
        local playerData = LoadKeyValues("C:/Users/Peter/Documents/Half-Life Alyx Multiplayer/Build/Client/temp-client/"..tostring(i)..".txt")
        if (playerData["IsLocal"] == "True") then
            print("Found my id")
            myId = i
        end
    end
    cvar_getf()
    SpawnPlayer(1)
end



function Update()

    if (file == null) then
        return
    end
    -- prints the player location and rotation to the console
    print("Transform Update: "..tostring(myId).." "..tostring(localPlayer:GetOrigin()).." "..tostring(localPlayer:GetAngles()))

    -- for k,v in pairs(EntityGroup) do
    --     if (k > file["playerCount"]) then
    --         v:SetOrigin(offScreenPos)
    --     end
    -- end

    for i=1, file["playerCount"] do
        local playerData = LoadKeyValues("C:/Users/Peter/Documents/Half-Life Alyx Multiplayer/Build/Client/temp-client/"..tostring(i)..".txt")

        if (playerData == null) then
            print("Couldn't find player data for "..tostring(i))
            return
        end

        -- if (playerData["IsLocal"] ~= "True") then
            local posString = playerData["Position"]
            posString = string.sub(posString, -posString:len(), -1)
            local components = {}
            for component in posString:gmatch("%w+") do table.insert(components, component) end
            local x = tonumber(components[1])
            local y = tonumber(components[2])
            local z = tonumber(components[3])

            pos = Vector(x, y, z)
            EntityGroup[i]:SetOrigin(pos)
            --EntityGroup[i]:SetAngles(playerData["angles"])
        -- end
    end
end

