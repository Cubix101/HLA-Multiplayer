# Half-Life: Alyx Multiplayer

## How does it work?
While Source 2 does have some limited networking capabilities, Valve is cringe and won't let us get our grubby little grabbers on it.

But I want HLA multiplayer.

So, here's how it works.
A C# client and server are ran seperately from HLA which can freely communicate however I want through sending/receiving packets, pretty standard networking stuff.
However, what's special is that the client can read data from the HLA console, which can then be sent to the server, and likewise lua can read data sent from the server using 'keyvalue' files.
So, using this (limited) functionality, there's a stable enough basis to make some limited multiplayer.
Is it great? No, absolutely not. Will we ever get full HLA multiplayer? Probably not, but right now this is the closest we can get (as far as I know)

## How do I use it?

You don't yet. This isn't ready for you to use. Go away.