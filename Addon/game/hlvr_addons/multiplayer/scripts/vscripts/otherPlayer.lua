local p_id
local username

PlayerGoal = Entities:FindByName(nil,"pm_goal")

--dont worry im gonna add more stuff to this function later
function UpdateLocation(x,y,z)
    pos = Vector(x,y,z)
    thisEntity:SetOrigin(pos)
end

function getID(p_id)
    print("ass2")
end

function OnSpawn()
    --set the username, for now all players will just be you. this is only for testing
    --update: this does not work. How to get username???
    --print(getStr(name[0]))
    UpdateLocation(500,500,30)
end