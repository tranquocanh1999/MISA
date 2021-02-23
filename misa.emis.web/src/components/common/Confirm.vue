<template>
  <div name="dialog" class="confirm">
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
        <div class="confirm__btn">
          <button
            class="btn-default"
            v-on:click="handleBackdropClick"
            v-on:keyup.13="handleBackdropClick"
            ref="confirmFocus"
          >
            Không
          </button>

          <button class="btn " v-on:click="onAccept" v-on:keyup.13="onAccept">
            Đồng ý
          </button>
        </div>
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

    onAccept: {
      type: Function,
      default: () => 1,
    },
  },
  methods: {
    handleBackdropClick() {
      this.$emit("update:active", false);
    },
  },
 async created() {
    setTimeout(() => {
      this.$refs.confirmFocus.focus();
    }, 0);
  },
};
</script>

<style></style>
