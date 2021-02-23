<template>
  <div name="dialog" class="alert">
    <div v-if="active" class="dialog-backdrop" @click="handleBackdropClick">
      <div class="dialog-container" @click.stop>
        <div
          class="icon__cancel"
          tabindex="1"
          v-on:click="handleBackdropClick"
          v-on:keyup.13="handleBackdropClick"
        ></div>
        <h1 class="dialog__title">Thông báo</h1>
        <div class="alert__text">{{ text }}</div>
        <button
          class="btn alert__btn"
          v-on:click="handleBackdropClick"
          v-on:keyup.13="handleBackdropClick"
          ref="focus"
        >
          Đồng ý
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
  props: {
    text: {
      type: String,
      default: "",
    },
    active: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    handleBackdropClick() {
      this.$emit("update:active", false);
    },
  },

  created() {
    setTimeout(() => {
      this.$refs.focus.focus();
    }, 0);
  },
};
</script>

<style></style>
