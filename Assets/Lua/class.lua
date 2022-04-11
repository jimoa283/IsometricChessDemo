local Class = {}

--[[
    -- @param _className ����
    -- @param  _base ���࣬����
]]
function BaseClass(_className, _base)
    local _class = {}
    Print_log('�½���BaseClass������' .. _className)
    _class.className = _className
    _class.base = _base
    -- ��������
    -- @param ...Ҫ���ݸ����캯���Ĳ����б�
    function cls.new(...)
        local instance = setmetatable({}, cls)
        instance.class = cls
        instance:ctor(...)
        return instance
    end

    local classTable = {}
    Class[_class] = classTable

    -- �������Ԫ��
    setmetatable(
        _class,
        {
            -- �������õ���ĳ�Ա�����ͷ�������ΪLua�����ı������ͱ�����ֵ������key��value��
            -- ͬ���ķ������ͷ�������Ҳ��key��value
            __newindex = function(_table, _key, _value)
                Print_log('����__newindex',',_key:', _key,',value:',_value)
                classTable[_key] = _value
            end,
            __index = classTable
        }
    )
    -- ���������Ա��Ԫ�������ʾ�������ĳ�������������������߷���
    -- luaһ��һ���ȥ���ң�����-����-����ĸ��ࣩ
    if _base then
        setmetatable(
            classTable,
            {
                __index = function(_talbe, _key)
                    Print_log('���������Ա��Ԫ��,table:',_talbe,'key:',_key)
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



