local Class = {}

--[[
    -- @param _className 类名
    -- @param  _base 基类，父类
]]
function BaseClass(_className, _base)
    local _class = {}
    Print_log('新建类BaseClass，类名' .. _className)
    _class.className = _className
    _class.base = _base
    -- 创建对象
    -- @param ...要传递给构造函数的参数列表
    function cls.new(...)
        local instance = setmetatable({}, cls)
        instance.class = cls
        instance:ctor(...)
        return instance
    end

    local classTable = {}
    Class[_class] = classTable

    -- 设置类的元表
    setmetatable(
        _class,
        {
            -- 这里设置的类的成员变量和方法，因为Lua会把类的变量名和变量的值看成是key和value，
            -- 同样的方法名和方法内容也是key和value
            __newindex = function(_table, _key, _value)
                Print_log('设置__newindex',',_key:', _key,',value:',_value)
                classTable[_key] = _value
            end,
            __index = classTable
        }
    )
    -- 索引基类成员的元表，这里表示的是如果某个对象调用了其变量或者方法
    -- lua一层一层的去查找（子类-父类-父类的父类）
    if _base then
        setmetatable(
            classTable,
            {
                __index = function(_talbe, _key)
                    Print_log('索引基类成员的元表,table:',_talbe,'key:',_key)
                    return Class[_base][_key]
                end
            }
        )
    end
    return _class
end

function Print_log(...)
   -- print(...)
end



