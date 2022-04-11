local  monoSingleton =BaseClass("MonoSingleton",MonoClass)

function Singleton:Instance()
	if rawget(self,"instance")==nil then
	   rawset(self,"instance",self.new())
	end
	assert(self.instance~=nil)
	return self.instance
end

return monoSingleton