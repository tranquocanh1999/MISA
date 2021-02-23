<template>
  <div class="tooltip">
    <div
      class="tooltip__icon"
      v-bind:style="{ backgroundImage: 'url(' + icon + ')',backgroundPositionX:backgroundPosition.x,backgroundPositionY:backgroundPosition.y }"
    ></div>
    <div class="tooltip__text">{{ content }}</div>
  </div>
</template>

<script>
export default {
  name: "Tooltip",
  props: {
    content: {
      type: String,
      default: "",
    },
    icon: {
      type: String,
      default: "",
    },
    backgroundPosition:{
      type: String,
      default: "",
    }
  },
};
</script>

<style>

</style>
