<template>
  <div class="navbar">
    <div class="navbar__header">
      <div class="logo"></div>
      <h3>Khoản thu</h3>
      <div class="toggle"></div>
    </div>
    <div class="navbar__menu">
      <div
        class="navbar__menu__item"
        :class="{active:index===active}"
        v-for="(item, index) in items"
        v-bind:key="index"
        v-on:click="onClick(index)"
      >
        <div
          class="item-icon"
          v-bind:style="{ backgroundImage: 'url(' + item.url + ')' }"
        ></div>
        <div class="item-text">{{ item.text }}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Navbar",
  props:{
      active:{
          type: Number,
          default:0
      },
      onClick:{
          type: Function,
          default: () =>1
      }

  },
  data() {
    return {
      items: [
        {
          url: require("../../assets/Resources/ImagesIcons/ic_TongQuan_32.svg"),
          text: "Tổng quan",
        },
        {
          url: require("../../assets/Resources/ImagesIcons/ic_LapKeHoachThu_32.svg"),
          text: "Lập kế hoạch thu",
        },
        {
          url: require("../../assets/Resources/ImagesIcons/ic_QuanLyThu_32.svg"),
          text: "Quản lý thu",
        },
        {
          url: require("../../assets/Resources/ImagesIcons/ic_HoaDon_32.svg"),
          text: "Quản lý hóa đơn",
        },
        {
          url: require("../../assets/Resources/ImagesIcons/ic_SoThu_32.svg"),
          text: "Số phải thu, phải trả",
        },
        {
          url: require("../../assets/Resources/ImagesIcons/ic_BaoCao_32.svg"),
          text: "Báo cáo",
        },
        {
          url: require("../../assets/Resources/ImagesIcons/ic_TinNhan_32.svg"),
          text: "Tin nhắn",
        },
        {
          url: require("../../assets/Resources/ImagesIcons/ic_HeThong_32.svg"),
          text: "Hệ thống",
        },
      ],
    };
  },
};
</script>

<style>
</style>
