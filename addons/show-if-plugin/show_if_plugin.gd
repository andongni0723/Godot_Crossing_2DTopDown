@tool
extends EditorPlugin

var a: int = 0

func can_handle(object):
    # 檢查是否能處理當前對象
    return object is Resource

func parse_begin(object):
    # 遍歷屬性列表，動態控制顯示
    var script = object.get_script()
    if not script or not script.has_method("_get_property_list"):
        return

    var property_list = object._get_property_list()
    for property in property_list:
        # 確保這是我們定義的屬性
        if property.has("usage") and property.usage & PROPERTY_USAGE_EDITOR:
            var meta = property.metadata
            if meta and meta.has("ShowIf"):
                var condition = meta["ShowIf"]
                if not _evaluate_condition(object, condition):
                    property["usage"] &= ~PROPERTY_USAGE_EDITOR
                    object.set(property.name, null)  # 將屬性移除

func _evaluate_condition(object, condition):
    # 根據條件判斷是否應該顯示
    if typeof(condition) == TYPE_DICTIONARY:
        var target_var = condition.get("TargetVar", null)
        var target_value = condition.get("TargetValue", null)
        if target_var and object.has(target_var):
            return object.get(target_var) == target_value
    return false