<template>
  <div class="feeList">
    <div class="feeList__tab">
      <div
        class="tab_item"
        :class="{ active: index === active }"
        v-for="(item, index) in tabItems"
        :key="index"
        v-on:click="onClick(index)"
      >
        {{ item.text }}
      </div>
    </div>
    <div class="feeList__feature">
      <CheckBox
        content="Hiển thị khoản thu ngừng theo dõi"
        styles="width:250px;"
        :checked="a"
        :value.sync="a"
      >
      </CheckBox>
      <div class="feature__right">
        <Button
          class="btn"
          content="Thêm mới"
          :onClick="onCreateFee"
          v-on:keyup.13="onCreateFee"
        ></Button>
        <Button class="btn-default" content="Sắp lại thứ tự" disabled></Button>
        <Button
          class="btn-default"
          :icon="
            require('../../../assets/Resources/ImagesIcons/ic_Remove2.svg')
          "
          :onClick="onMultiDelete"
        ></Button>
      </div>
    </div>
    <div class="feeList__table">
      <table>
        <thead>
          <tr>
            <th>
              <CheckBox
                :style="{
                  width: '40px',
                  marginLeft: '15px',
                  marginTop: '-15px',
                }"
                value="false"
                disabled
              />
            </th>
            <th
              v-for="item in thead"
              v-bind:key="item.className"
              v-bind:class="item.className"
              v-bind:style="item.style"
            >
              {{ item.content }}
              <br />
              <InputVue
                :type="text"
                :label="''"
                :prefix="
                  require('../../../assets/Resources/ImagesIcons/ic_Filter.svg')
                "
                :style="{ width: '100%', height: '28px;', margin: '0 8px' }"
                v-if="item.hasFilter & (item.className !== 'Period')"
              />

              <ComboBox
                :listData="period"
                :style="{
                  marginLeft: '8px',
                  marginRight: '-28px',
                  width: '180px',
                  marginBottom: '12px',
                }"
                v-if="item.hasFilter & (item.className === 'Period')"
              ></ComboBox>
            </th>
          </tr>
        </thead>
        <tbody class="table_body">
          <tr v-for="(fee, index) in tbody" v-bind:key="fee.feeID">
            <td>
              <CheckBox
                :style="{
                  width: '40px',
                  marginLeft: '15px',
                  marginTop: '-40%',
                }"
                :checked.sync="listDelete[index]"
                :value="index"
                v-bind:onClick="onAddListDelete"
              />
            </td>

            <td>
              <div class="feeName">
                <div class="feeName__text">{{ fee.feeName }}</div>
                <div class="feeName__tooltip">
                  <Tooltip
                    :icon="
                      require('../../../assets/Resources/ImagesIcons/ic_sprites2.png')
                    "
                    :backgroundPosition="{ x: '-97px', y: '-336px' }"
                    content="Đây là khoản thu mặc định của hệ thống, bạn không thể xóa"
                    v-if="fee.isSystem"
                  />
                </div>
              </div>
            </td>
            <td></td>
            <td>{{ fee.price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') }}/{{ unit(fee.unit) }}</td>
            <td>{{ Period(fee.period) }}</td>
            <td>{{ paymentDeadline(fee.period) }}</td>
            <td><div :class="{ checked: fee.isApplyRemisson }" /></td>
            <td><div :class="{ checked: fee.allowCreateInvoice }" /></td>
            <td><div :class="{ checked: fee.allowCreateReceipt }" /></td>
            <td><div :class="{ checked: fee.allowRefunds }" /></td>
            <td><div :class="{ checked: fee.isRequire }" /></td>
            <td><div :class="{ checked: fee.isActive }" /></td>
            <td>
              <div class="fee__tool">
                <div class="fee__edit" @click="onEdit(fee.feeID)"></div>
                <div
                  class="fee__duplicate"
                  @click="onDuplicate(fee.feeID)"
                ></div>
                <div class="fee__delete" @click="onDelete(fee.feeID)"></div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="feeList__footer">
      Tổng số: <b>{{ count }}</b> kết quả
    </div>
    <FeeDetail
      :active.sync="isAdd"
      :title="feeDetailsContent"
      :fee.sync="fee"
      :onSubmit="onSubmit"
      :errorMess="errorMess"
      v-if="isAdd"
    ></FeeDetail>
  </div>
</template>

<script>
import * as axios from "axios";
import FeeDetail from "./FeeDetail";
import Tooltip from "../../../components/common/Tooltip";
import feeList from "../../../assets/feeList.JSON";
import period from "../../../assets/period.JSON";
import InputVue from "../../../components/common/InputVue";
import CheckBox from "../../../components/common/CheckBox";
import Button from "../../../components/common/Button";
import ComboBox from "../../../components/common/ComboBox";
export default {
  data() {
    return {
      tabItems: [
        {
          text: "Quy trình",
        },
        {
          text: "Danh sách khoản thu",
        },
        {
          text: "Đăng ký khoản thu",
        },
        {
          text: "Đăng ký miễn giảm",
        },
      ],
      thead: feeList.thead,
      tbody: feeList.tbody,
      a: true,
      period: period,
      listDelete: feeList.listDelete,
      isAdd: false,
      feeDetailsContent: "",
      count: 0,
      groups: [],
      feeNull: {
        feeID: -1,
        feeName: "",
        feeGroupID: -1,
        price: 0,
        unit: -1,
        applyObject: "0",
        property: 1,
        period: 1,
        isApplyRemisson: true,
        isRequire: false,
        allowCreateInvoice: false,
        allowCreateReceipt: false,
        isActive: false,
        isInternal: false,
        isSystem: false,
        createdDate: "2021-01-20T21:56:39.807",
        createdBy: null,
        modifiedDate: "2021-01-20T21:56:39.807",
        modifiedBy: null,
      },
      fee: [],
      errorMess: {
        feeNameErrMess: "",
        feeGroupErrMess: "",
        priceErrMess: "",
        unitErrMess: "",
        applyObjectErrMess: "",
        periodErrMess: "",
      },
      isEdit: false,
    };
  },
  props: {
    active: {
      type: Number,
      default: 0,
    },
    onClick: {
      type: Function,
      default: () => 1,
    },
  },

  components: {
    CheckBox,
    Button,
    InputVue,
    ComboBox,
    Tooltip,
    FeeDetail,
  },
  methods: {
    unit(unit) {
      if (unit === 1) return "ngày";
      if (unit === 2) return "tháng";
      if (unit === 3) return "quý";
      if (unit === 4) return "kỳ";
      if (unit === 5) return "năm";
      return "";
    },
    Period(period) {
      if (period === 1) return "Tháng";
      if (period === 2) return "Quý";
      if (period === 3) return "Học Kỳ";
      if (period === 4) return "Năm Học";
      return "";
    },
    paymentDeadline(period) {
      if (period === 1) return "Hàng Tháng";
      if (period === 2) return "Hàng Quý";
      if (period === 3) return "Tháng 9";
      if (period === 4) return "Hàng Năm";
      return "";
    },

    onEdit(id) {
      axios.get("http://localhost:49682/api/v1/Fees/" + id).then((response) => {
        this.fee = response.data;
        this.feeDetailsContent = "Chỉnh sửa khoản thu";
        this.isEdit = true;
        this.isAdd = true;
      });
    },
    onDelete(id) {
      if (window.confirm("bạn có chắc muốn xóa khoản thu này")) {
        
        console.log(id);
        axios.delete("http://localhost:49682/api/v1/Fees/" + id.toString())
        .then(() => {
              location.reload();
            })
            .catch((error) => {
             
              alert(error.response.data.userMsg);
            });
      }
    },
    onDuplicate(id) {
      alert(id + "du");
    },
    onMultiDelete() {
      for (var i = 0; i < this.tbody.length; i++) {
        if (this.listDelete[i] && this.tbody[i].isSystem) {
          console.log("có khoản thu của hệ thống");
        }
        if (this.listDelete[i] && !this.tbody[i].isSystem) {
          console.log(this.tbody[i].FeeID);
        }
      }
    },
    onAddListDelete(index) {
      this.listDelete[index] = !this.listDelete[index];
    },
    onCreateFee() {
      this.fee = this.feeNull;
      this.feeDetailsContent = "Thêm mới khoản thu";
      this.isEdit = false;
      this.isAdd = true;
    },
    validate(data) {
      console.log(data);
      if (data.indexOf("Tên khoản thu bị trống, vui lòng kiểm tra lại") !== -1)
        this.errorMess.feeNameErrMess =
          "Tên khoản thu bị trống, vui lòng kiểm tra lại";
      if (data.indexOf("Tên khoản thu bị trùng, vui lòng kiểm tra lại") !== -1)
        this.errorMess.feeNameErrMess =
          "Tên khoản thu bị trùng, vui lòng kiểm tra lại";

      if (
        data.indexOf("Mức thu  bị trống hoặc <0 vui lòng kiểm tra lại") !== -1
      )
        this.errorMess.priceErrMess =
          "Mức thu  bị trống hoặc <=0 vui lòng kiểm tra lại";
      if (
        data.indexOf(
          "Mã Đơn vị thu bị trống hoặc <=0, vui lòng kiểm tra lại"
        ) !== -1
      )
        this.errorMess.unitErrMess = "đơn vị thu không được để trống";
    },
    onSubmit(e) {
      if (this.isEdit) {
        if (window.confirm("bạn có chắc muốn sửa khoản thu này")) {
          axios
            .put("http://localhost:49682/api/v1/Fees/" + e.feeID, e)
            .then(() => {
              location.reload();
            })
            .catch((error) => {
              console.log(error.response)
              this.validate(error.response.data.userMsg);
            });
        }
      } else {
        if (window.confirm("bạn có chắc muốn thêm mới khoản thu")) {
          if (e.createdDate === "") e.createdDate;
          delete e["feeID"];
          e.applyObject = e.applyObject.toString();
          console.log(e);
          axios
            .post("http://localhost:49682/api/v1/Fees", e)
            .then(() => {
              location.reload();
            })
            .catch((error) => {
              this.validate(error.response.data.userMsg);
            });
        }
      }
    },
  },
  async created() {
    const response = await axios.get("http://localhost:49682/api/v1/Fees");
    this.tbody = response.data;
    this.count = this.tbody.length;
    var list = new Array();
    for (var i = 0; i < this.count; i++) {
      list.push(false);
    }
    this.listDelete = list;
    const responses = await axios.get(
      "http://localhost:49682/api/v1/fee-groups"
    );
    this.groups = responses.data;
  },
};
</script>

<style></style>
