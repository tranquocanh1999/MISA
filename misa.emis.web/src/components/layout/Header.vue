<template>
  <div class="header">
    <div class="header__title">
      <h3 class="title__text">Lập kế hoạch thu</h3>
      <Tooltip
        :icon="require('../../assets/Resources/ImagesIcons/ic_sprites2.png')"
        :backgroundPosition="{ x: '-912px', y: '-48px' }"
        content="Đây là tooltip cho lập kế hoạch thu"
      />
    </div>
    <div class="header__left">
 
        <div class="icon-home" />
        <InputVue
          type="text"
          placeholder="[Thí điểm] Trường mẫu giáo hoa hồng"
          :style="{ width: '300px', marginBottom: '12px' }"
     
      />
      <ComboBox id="time" :listData="data" style="marginBottom:12px" />
      <div class="header__search"></div>
      <div class="header__avatar"></div>
    </div>
  </div>
</template>

<script>
import Tooltip from "../common/Tooltip";
import ComboBox from "../common/ComboBox";
import InputVue from "../common/InputVue";
export default {
  name: "Header",
  data() {
    return {
      data: [
        { id: 1, text: "2012-2013" },
        { id: 2, text: "2013-2014" },
        { id: 3, text: "2014-2015" },
        { id: 4, text: "2015-2016" },
        { id: 5, text: "2016-2017" },
      ],
    };
  },
  components: {
    Tooltip,
    ComboBox,
    InputVue,
  },
};
</script>

<style>
</style>
