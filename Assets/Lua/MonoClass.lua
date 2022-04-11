local monoClass=BaseClass("MonoClass")

monoClass.gameObject=nil;
monoClass.transform=nil;

function monoClass:ctor(gameObject)
	self.gameObject=gameObject
	self.transform=gameObject.transform
end


function monoClass:Awake( )
	
end

function monoClass:Start( )
	
end

function  monoClass:Update( )
	
end

function monoClass:OnDestory()
	
end

return monoClass
