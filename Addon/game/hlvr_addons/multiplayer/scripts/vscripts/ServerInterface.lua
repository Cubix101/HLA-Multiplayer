local serverInterface = {}

-- Args should be seperated by a comma with no space, e.g: 1.4,2.5,3.1
-- Data type is an integer representing what type of data this is so the client knows how to read it. Data types are:
-- 0: player data: an integer, representing player ID, a vector, representing position, and another vector, representing angles
function serverInterface.SendData (dataType, args)
    local dataString = "serverMessage: "..tostring(dataType).." ".."["..args.."]"

    print(dataString)
end

return serverInterface