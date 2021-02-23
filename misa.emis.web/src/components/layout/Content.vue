<template>
  <div class="content">
    <FeeList
      :active="active"
      :onClick="
        (index) => {
          this.onClick(index);
        }
      "
      :onShowDialog="onShowDialog"
    ></FeeList>
  </div>
</template>

<script>
import FeeList from "../../views/dictionary/revenueList/FeeList";
export default {
  name: "Content",
  props: {
    active: {
      type: Number,
      default: 0,
    },
    onClick: {
      type: Function,
      default: () => 1,
    },
    onShowDialog: {
      type: Function,
      default: () => 1,
    },
  },

  methods: {},
  components: {
    FeeList,
  },
};
</script>

<style>

</style>
