local myId
local file
local offScreenPos
local localPlayer

-- moist fellow here. bme is once again being cringe so i have taken it upon myself to try to add comments to this shit while trying to figure out how it works

function Start ()
    offScreenPos = EntityGroup[1]:GetOrigin()
    localPlayer = Entities:GetLocalPlayer()
    file = LoadKeyValues("C:/Users/Peter/Documents/Half-Life Alyx Multiplayer/Build/Client/temp-client/GameInfo.txt")
    -- this gives you an id (unique to your player)
    for i=1, file["playerCount"] do
        local playerData = LoadKeyValues("C:/Users/Peter/Documents/Half-Life Alyx Multiplayer/Build/Client/temp-client/"..tostring(i)..".txt")
        if (playerData["IsLocal"] == "True") then
            print("Found my id")
            myId = i
        end
    end
end

--spawn a new player
function SpawnPlayer
    DoEntFire("p_spawn_relay","Trigger")
end

function Update ()

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

